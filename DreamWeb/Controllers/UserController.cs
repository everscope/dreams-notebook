using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DreamWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace DreamWeb.Controllers
{
    public class UserController : Controller
    {

        private DreamInput _dreamService;
        private DreamsContext _dreamsContext;

        public UserController (DreamInput dreamService, DreamsContext dbContext)
        {
            _dreamService = dreamService;
            _dreamsContext = dbContext;
        }

        [Authorize]
        public IActionResult User()
        {
            ViewBag.Context = _dreamsContext.Users.First(p => p.UserName == HttpContext.User.Identity.Name);
            return View();
        }

        [HttpPost]
        public async Task<string> AddDream(string dreamName, string topics, string hours,
                                          string[] content, string externalId, bool isPublic,
                                          string authorId)
        {
            _dreamService.Name = dreamName;
            _dreamService.Topics = topics;
            _dreamService.Hours = hours;
            _dreamService.Content = content;
            _dreamService.ExternalId = externalId;
            _dreamService.IsPublic = isPublic;
            _dreamService.AuthorId = authorId;

            await _dreamService.AddDreamAsync();
            return "true";    
        }

    }
}
