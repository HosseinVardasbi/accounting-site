using MediatR;

namespace HamedProject02.Features.Users.Commands.Insert
{
    public class InsertUserCommand : IRequest<DataLayer02.Domain.User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public InsertUserCommand(string userName, string password, string email, bool active, DateTime created)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Active = active;
            Created = created;
        }
    }
}
