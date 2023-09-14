using DataLayer02.Domain;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Users.Queries.Select
{
    public class GetUserListHandler : IRequestHandler<GetUserListQuery, IEnumerable<DataLayer02.Domain.User>>
    {
        IUserRepository userRepository;
        public GetUserListHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUsers();
        }
    }
}
