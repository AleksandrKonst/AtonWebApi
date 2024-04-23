using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands;

public static class CreateUser
{
    public record Command(NewUserDto NewUserDto, string UserLogin) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.NewUserDto.Login)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина");
            
            RuleFor(x => x.NewUserDto.Password)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат пароля");
            
            RuleFor(x => x.NewUserDto.Name)
                .Matches("^[a-zA-Zа-яА-Я0-9]*$")
                .WithMessage("Неверный формат имени");
            
            RuleFor(x => x.NewUserDto.Gender)
                .GreaterThanOrEqualTo(0)
                .LessThan(3)
                .WithMessage("Неверный формат имени");
            
            RuleFor(x => x.UserLogin)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина пользователя");
        }
    }
    
    public class Handler(IUserRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request.NewUserDto);
            user.CreatedBy = request.UserLogin;
            
            var result = await repository.AddAsync(user);
            logger.LogInformation($"Create {nameof(user.Login)}");
            
            return new CommandResult(result);
        }
    }
}