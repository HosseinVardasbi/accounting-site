using DataLayer02.Domain;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Users.Queries.SelectById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, DataLayer02.Domain.User>
    {
        IUserRepository userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserById(request.Id);
        }
    }
}
