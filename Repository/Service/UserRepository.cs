using DataLayer02.Context;
using DataLayer02.Domain;
using HamedProject02.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HamedProject02.Repository.Service
{
    public class UserRepository : IUserRepository
    {
        DB_context db;
        public UserRepository(DB_context db)
        {
            this.db = db;
        }

        public async Task<User> AddUser(User user)
        {
            var result = db.users.Add(user);
            db.SaveChanges();
            return result.Entity;
        }

        public async Task<User> GetUserById(int id)
        {
            return (from p in db.users
                    where p.Id == id
                    select new User
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        Email = p.Email,
                        Created = p.Created,
                        Active = p.Active
                    }).FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = db.users.Include(p=>p.Role).ToList();

            
            return (from p in db.users select new User { Id = p.Id, UserName = p.UserName,
            Email = p.Email, Created = p.Created, Active = p.Active, RoleId = p.RoleId}).ToList();
        }

        public async Task<User> SignIn1(string UserName, string Password)
        {
            var userId = db.users.Where(x => x.UserName == UserName && x.Password == Password
            && x.Active).SingleOrDefault();
            //if (db.users.Any(x => x.UserName == UserName && x.Password == Password
            //&& x.Active))
            //    return await db.users.SingleOrDefaultAsync(x => x.UserName == UserName && x.Password == Password
            //&& x.Active);
            if(userId == null)
            {
                return null;
            }
            return userId;

            //return new User() {
            //    Id = -1
            //};
        }


        public User SignIn(string UserName, string Password)
        {
            var userId = db.users.Include(p=>p.Role).SingleOrDefault(x => x.UserName == UserName && x.Password == Password
            && x.Active);
            if (userId == null)
            {
                return null;
            }
         //   var tokenHandler = new JwtSecurityTokenHandler();
          //  var key = Encoding.ASCII.GetBytes("mySecreat");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name,userId.Id.ToString()),
            new Claim(ClaimTypes.Role,userId.Role.RoleName)

            }),
                Expires = DateTime.UtcNow.AddDays(1),
             //   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        //    var token=tokenHandler.CreateToken(tokenDescriptor);
            return userId;
        }

        public async Task<int> UpdateUser(User user)
        {
            int result = 0;
            if(db != null)
            {
                db.users.Update(user);
                result = db.SaveChanges();
                return result;
            }
            return result;
        }

        
    }
}
