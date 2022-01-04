using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamWeb.Models;

namespace DreamWeb.Controllers
{
    public class SignUpController : Controller
    {

        private IUserService _signUp;

        public SignUpController(IUserService signUp)
        {
            
            _signUp = signUp;
        }


        [HttpPost]
        public async Task<IActionResult> CheckInputData(NewUser newUser)
        {
            string result = await _signUp.CreateNewAccount(newUser);
            if (result == null)
            {
                return RedirectToActionPermanent("SignUp", new { resultMessage = "You've created an account", i = true});
            }
            else
            {
                return RedirectToActionPermanent("SignUp", new { resultMessage = result, i = false }) ;
            }

        }

        public IActionResult SignUp(string resultMessage, bool i)
        {
            ViewBag.Error = resultMessage;
            ViewBag.Success = i;
            if(_signUp.GetNewUser() != null)
            {
                ViewBag.login = _signUp.GetNewUser().Login;
                ViewBag.password = _signUp.GetNewUser().Password;
                ViewBag.repassword = _signUp.GetNewUser().Repassword;
                ViewBag.email = _signUp.GetNewUser().Email;
                ViewBag.externalId = _signUp.GetNewUser().ExteranlId;
            }

            return View();
        }
    }
}
