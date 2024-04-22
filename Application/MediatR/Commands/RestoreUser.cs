using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class RestoreUser
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
        }
    }
    
    public class Handler(IUserRepository repository, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.Login);
            if (user == null) throw new Exception("Пользователь не найден");
            
            user.RevokedOn = null;
            user.RevokedBy = null;

            var result = await repository.UpdateAsync(user);
            logger.LogInformation($"Restore {nameof(user.Login)}");

            return new CommandResult(result);
        }
    }
}