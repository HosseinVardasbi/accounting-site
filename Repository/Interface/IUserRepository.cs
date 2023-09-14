using DataLayer02.Domain;

namespace HamedProject02.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task<int> UpdateUser(User user);
        User SignIn(string UserName, string Password);
    }
}
