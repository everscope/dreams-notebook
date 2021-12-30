using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DreamWeb.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult User()
        {
            return View();
        }
    }
}
