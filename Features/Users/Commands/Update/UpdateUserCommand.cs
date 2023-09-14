using MediatR;

namespace HamedProject02.Features.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public UpdateUserCommand(int id, string userName, string password, string email, bool active, DateTime created)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Email = email;
            Active = active;
            Created = created;
        }
    }
}
