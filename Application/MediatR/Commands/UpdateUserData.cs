using Application.DTO;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class UpdateUserData
{
    public record Command(UserDataDto UserDataDto, string UserLogin) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserDataDto.Name)
                .Matches("^[a-zA-Zа-яА-Я0-9]*$")
                .WithMessage("Неверный формат имени");

            RuleFor(x => x.UserDataDto.Gender)
                .GreaterThanOrEqualTo(0)
                .LessThan(3)
                .WithMessage("Неверный формат имени");
        }
    }
    
    public class Handler(IUserRepository repository, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser =  await repository.GetAsync(request.UserLogin);
            if (currentUser == null || 
                (currentUser.Admin == false && currentUser.Login != request.UserDataDto.Login) || 
                currentUser.RevokedOn == null) 
                throw new Exception("Ошибка доступа");
            
            var user = await repository.GetAsync(request.UserDataDto.Login);

            if (user == null) throw new Exception("Пользователь не найден");
            
            user.Name = request.UserDataDto.Name;
            user.Gender = request.UserDataDto.Gender;
            user.Birthday = request.UserDataDto.Birthday;
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = request.UserLogin;

            var result = await repository.UpdateAsync(user);
            logger.LogInformation($"Update {nameof(user.Login)}");

            return new CommandResult(result);
        }
    }
}