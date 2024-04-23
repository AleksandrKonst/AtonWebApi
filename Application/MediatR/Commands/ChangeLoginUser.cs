using Application.Infrastructure;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class ChangeLoginUser
{
    public record Command(string Login, string UserLogin) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Login)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина");
            
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
            await UserInfrastructure.CheckUser(request.Login, request.UserLogin);

            var user = await repository.GetAsync(request.Login);
            if (user == null) throw new Exception("Пользователь не найден");
            
            user.Login = request.Login;
            user.ModifiedOn = DateTime.Now.ToUniversalTime();
            user.ModifiedBy = request.UserLogin;

            var result = await repository.UpdateAsync(user);
            logger.LogInformation($"ChangeLogin {nameof(user.Login)}");

            return new CommandResult(result);
        }
    }
}