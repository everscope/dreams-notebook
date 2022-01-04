using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DreamWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DreamWeb.Controllers
{
    public class SignInController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;
        private IUserService _signIn;

        public SignInController (IUserService SignIn, UserManager<UserAccount> UserManager)
        {
            _signIn = SignIn;
            _userManager = UserManager;
        }

        [HttpGet("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }
        
        [HttpPost("SignIn")]
        //public async Task<string> SignIn(string login, string password)
        //{
        //    if(_signIn.GetSignInClaims(login, password) != null)
        //    {
        //        await HttpContext.SignInAsync(_signIn.GetSignInClaims(login, password));
        //        //return RedirectToAction("Index", "Home");
   
        //        return "success " + User.Identity.IsAuthenticated;
        //    }
        //    return "fail";
        //    //return RedirectToAction("Index", "Home");
        //}
        public async Task<string> SignIn(string login, string password)
        {
            return await _signIn.SignIn(login, password) + User.Claims.ToString();
            //return RedirectToAction("SignIn");
        }

    }
}
