using Application.DTO;
using Application.Infrastructure;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class UpdateUserData
{
    public record Command(NewUserDataDto NewUserDataDto, string UserLogin) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.NewUserDataDto.Name)
                .Matches("^[a-zA-Zа-яА-Я0-9]*$")
                .WithMessage("Неверный формат имени");

            RuleFor(x => x.NewUserDataDto.Gender)
                .GreaterThanOrEqualTo(0)
                .LessThan(3)
                .WithMessage("Неверный формат имени");
            
            RuleFor(x => x.UserLogin)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина пользователя");
        }
    }
    
    public class Handler(IUserRepository repository, ILogger<Handler> logger)
        : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            await UserInfrastructure.CheckUser(request.NewUserDataDto.Login, request.UserLogin);
            
            var user = await repository.GetAsync(request.NewUserDataDto.Login);
            if (user == null) throw new Exception("Пользователь не найден");
            
            user.Name = request.NewUserDataDto.Name;
            user.Gender = request.NewUserDataDto.Gender;
            user.Birthday = request.NewUserDataDto.Birthday;
            user.ModifiedOn = DateTime.Now.ToUniversalTime();
            user.ModifiedBy = request.UserLogin;

            var result = await repository.UpdateAsync(user);
            logger.LogInformation($"Update {nameof(user.Login)}");

            return new CommandResult(result);
        }
    }
}