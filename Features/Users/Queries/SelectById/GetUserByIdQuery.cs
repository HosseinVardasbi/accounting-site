using MediatR;

namespace HamedProject02.Features.Users.Queries.SelectById
{
    public class GetUserByIdQuery : IRequest<DataLayer02.Domain.User>
    {
        public int Id { get; set; }
    }
}
