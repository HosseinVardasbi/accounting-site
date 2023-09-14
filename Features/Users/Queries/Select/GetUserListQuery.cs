using MediatR;

namespace HamedProject02.Features.Users.Queries.Select
{
    public class GetUserListQuery : IRequest<IEnumerable<DataLayer02.Domain.User>>
    {
    }
}
