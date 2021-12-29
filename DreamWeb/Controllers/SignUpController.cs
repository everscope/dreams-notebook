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
        public string CheckInputData(NewUser newUser)
        {
            return _signUp.AddNewAccount(newUser) ?? "Everything is ok";

        }

    }
}
