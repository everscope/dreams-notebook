using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DreamWeb.Models;
using Microsoft.AspNetCore.Authentication;


namespace DreamWeb.Controllers
{
    public class SignInController : Controller
    {

        private IUserService _signIn;

        public SignInController (IUserService SignIn)
        {
            _signIn = SignIn;
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
        public IActionResult SignIn(string login, string password)
        {
            return RedirectToAction("SignIn");
        }

    }
}
