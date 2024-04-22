using Application.DTO;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Queries;

public static class GetUserByLoginAndPassword
{
    public record Query(string Login, string Password) : IRequest<QueryResult>;
    public record QueryResult(IEnumerable<UserByLoginDto> Result);
    
    public class Validator : AbstractValidator<Query>
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
    
    public class Handler(IUserRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await repository.CheckUserPasswordAsync(request.Login, request.Password);

            if (user == null)
            {
                throw new Exception("Неверный пароль или логин");
            }
            
            return new QueryResult(mapper.Map<IEnumerable<UserByLoginDto>>(user));
        }
    }
}