using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class DeleteUser
{
    public record Command(string Login, string UserLogin, bool Soft) : IRequest<CommandResult>;
    
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

    public class Handler(IUserRepository repository, IMapper mapper, ILogger<Handler> logger) 
        : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.Login);
            if (user == null) throw new Exception("Пользователь не найден");

            if (request.Soft)
            {
                user.RevokedOn = DateTime.Now;
                user.RevokedBy = request.UserLogin;
                
                var result = await repository.UpdateAsync(user);
                logger.LogInformation($"SoftDelete {nameof(user.Login)}");
                
                return new CommandResult(result);
            }
            else
            {
                var result = await repository.DeleteAsync(user);
                logger.LogInformation($"HardDelete {nameof(user.Login)}");
                
                return new CommandResult(result);
            }
        }
    }
}