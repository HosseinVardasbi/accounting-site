using HamedProject02.Repository.Interface;
using MediatR;

namespace HamedProject02.Features.Users.Commands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, int>
    {
        IUserRepository userRepository;
        public UpdateUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserById(request.Id);
            if (user == null)
                return default;
            user.Active = request.Active;
            return await userRepository.UpdateUser(user);
        }
    }
}
