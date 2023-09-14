using DataLayer02.Domain;
using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Users.Commands.Insert
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, DataLayer02.Domain.User>
    {
        IUserRepository userRepository;
        public InsertUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var users = new DataLayer02.Domain.User() {
            UserName = request.UserName,
            Password = request.Password,
            Email = request.Email,
            Active = request.Active,
            Created = request.Created};
            return await userRepository.AddUser(users);
        }
    }
}
