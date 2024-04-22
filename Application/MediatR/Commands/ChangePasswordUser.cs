using Application.DTO;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class ChangePasswordUser
{
    public record Command(ChangePasswordDto ChangePasswordDto, string UserLogin) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ChangePasswordDto.Login)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина");
            
            RuleFor(x => x.ChangePasswordDto.Password)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат пароля");
        }
    }

    public class Handler
        (IUserRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await repository.GetAsync(request.UserLogin);
            if (currentUser == null ||
                (currentUser.Admin == false && currentUser.Login != request.ChangePasswordDto.Login))
                throw new Exception("Ошибка доступа");

            var user = await repository.GetAsync(request.ChangePasswordDto.Login);

            if (user == null) throw new Exception("Пользователь не найден");
            
            user.Password = request.ChangePasswordDto.Password;
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = request.UserLogin;

            var result = await repository.UpdateAsync(user);
            logger.LogInformation($"ChangePassword {nameof(user.Login)}");

            return new CommandResult(result);
        }
    }
}