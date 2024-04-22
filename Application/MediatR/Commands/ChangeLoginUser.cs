using AutoMapper;
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
            
        }
    }

    public class Handler
        (IUserRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await repository.GetAsync(request.UserLogin);
            if (currentUser == null ||
                (currentUser.Admin == false && currentUser.Login != request.Login))
                throw new Exception("Ошибка доступа");

            var user = await repository.GetAsync(request.Login);

            if (user == null) throw new Exception("Пользователь не найден");
            
            user.Login = request.Login;
            user.ModifiedOn = DateTime.Now;
            user.ModifiedBy = request.UserLogin;

            var result = await repository.UpdateAsync(user);
            logger.LogInformation($"ChangeLogin {nameof(user.Login)}");

            return new CommandResult(result);
        }
    }
}