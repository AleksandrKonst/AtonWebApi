using Application.DTO;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.MediatR.Queries;

public static class GetUserByLogin
{
    public record Query(string Login) : IRequest<QueryResult>;
    public record QueryResult(IEnumerable<UserByLoginDto> Result);
    
    public class Handler(IUserRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<UserByLoginDto>>(await repository.GetAsync(request.Login)));
        }
    }
}