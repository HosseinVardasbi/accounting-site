using HamedProject02.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HamedProject02.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserRepository _userRepository;
        readonly IMediator mediator;
        public AccountController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            this.mediator = mediator;
        }

        public async Task<IActionResult> SignOut()
        {

            // kills the login cookie 
            await HttpContext.SignOutAsync();
              //  CookieAuthenticationDefaults.AuthenticationScheme);

            // redirect to web home or login page 
            return LocalRedirect("/");

        }

        
        public IActionResult SignIn()
        {
            if (HttpContext.User.Claims.Any())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
            //Session kalsm = "fgfgfg";
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> SignIn(string UserName, string Password)
        {
            

            var user = _userRepository.SignIn(UserName, Password);

            if (user != null)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                 };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                _ = HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }

            return View(user);



            //var user =  _userRepository.SignIn(UserName, Password);
            //if (user == null)
            //{
            //    return BadRequest(new { message = "incorrect" });
            //}
            //return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
