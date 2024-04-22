using Application.DTO;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.MediatR.Queries;

public static class GetAllRevokedOnUsers
{
    public record Query() : IRequest<QueryResult>;
    public record QueryResult(IEnumerable<UserDto> Result);
    
    public class Handler(IUserRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<UserDto>>(await repository.GetUsersByRevokedOnAsync()));
        }
    }
}