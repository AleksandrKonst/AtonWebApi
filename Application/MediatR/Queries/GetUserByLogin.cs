using Application.DTO;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Queries;

public static class GetUserByLogin
{
    public record Query(string Login) : IRequest<QueryResult>;
    public record QueryResult(UserByLoginDto Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Login)
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Неверный формат логина");
        }
    }
    
    public class Handler(IUserRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<UserByLoginDto>(await repository.GetAsync(request.Login)));
        }
    }
}