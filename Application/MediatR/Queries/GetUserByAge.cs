using Application.DTO;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.MediatR.Queries;

public static class GetUserByAge
{
    public record Query(int Age) : IRequest<QueryResult>;
    public record QueryResult(IEnumerable<UserDto> Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0)
                .WithMessage("Неверный формат даты рождения");
        }
    }
    
    public class Handler(IUserRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<UserDto>>(await repository.GetUsersByAgeAsync(request.Age)));
        }
    }
}