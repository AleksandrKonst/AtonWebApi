using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class LoginUser
{
    public record Command(string Login, string Password) : IRequest<CommandResult>;
    
    public record CommandResult(bool Admin);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Login)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина");
            
            RuleFor(x => x.Password)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат пароля");
        }
    }

    public class Handler(IUserRepository repository, ILogger<Handler> logger)
        : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await repository.CheckUserPasswordAsync(request.Login, request.Password);
            if (user == null) throw new Exception("Неверный пароль или логин");
            
            logger.LogInformation($"Enter in system {nameof(user.Login)}");

            return new CommandResult(user.Admin);
        }
    }
}