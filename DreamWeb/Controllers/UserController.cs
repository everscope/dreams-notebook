using DreamWeb.DAL;
using DreamWeb.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DreamWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace DreamWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private DreamInput _dreamService;
        private IDatabaseReader _databaseReader;

        public UserController (DreamInput dreamService, IDatabaseReader databaseReader)
        {
            _dreamService = dreamService;
            _databaseReader = databaseReader;
        }

        [Authorize]
        public async Task<IActionResult> User()
        {
            //var user = _dreamsContext.Users.First(p => p.UserName == HttpContext.User.Identity.Name)

            var user = await _databaseReader.GetUserAccountAsync(HttpContext.User.Identity.Name);
            user.Dreams.OrderByDescending(p => p.CreationDate);

            ViewBag.User = user;
            //if (_dreamsContext.DreamPublications.Any(p => p.AuthorID == user.Id))
            //{
            //    var content = _dreamsContext.DreamPublications.Where(p => p.AuthorID == user.Id);
            //    content.OrderByDescending(p => p.CreationDate);

            //    List <DreamPublication> publications = new List<DreamPublication>();
            //    foreach (DreamPublication result in content)
            //    {
            //        publications.Add(result);
            //    }
            //    ViewBag.Content = publications;
            //}
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DreamsSort(int orderBy, DateTime date, string keyWords)
        {
            var user = await _databaseReader.GetUserAccountAsync(HttpContext.User.Identity.Name);
            //var user = _dreamsContext.Users.First(p => p.UserName == HttpContext.User.Identity.Name);
            //var content = _dreamsContext.DreamPublications.Where(p => p.AuthorID == user.Id);
            ViewBag.UserContext = user;

            switch (orderBy)
            {
                case 1:
                    content = content.OrderByDescending(p => p.CreationDate);
                    break;
                case 2:
                    content = content.OrderBy(p => p.CreationDate);
                    break;
                case 3:
                    content = content.OrderBy(p => p.Content.Length);
                    break;
                case 4:
                    content = content.OrderByDescending(p => p.Content.Length);
                    break;
                default:
                    content = content.OrderByDescending(p => p.CreationDate);
                    break;
            }

            if (date != new DateTime(0001, 01, 01))
            {
                content = content.Where(p => p.CreationDate.Date == date.Date);
            }

            if(keyWords != null)
            {
                List<DreamPublication> result = new List<DreamPublication>();


                string[] keys = keyWords.Split(new char[] { ' ', ',', '.' });

                var resultTemp = content.Where(p => p.Content.Contains(keyWords));
                
                foreach(DreamPublication dream in resultTemp)
                {
                    result.Add(dream);
                }

                foreach(string key in keys)
                {
                    var thisSearch = content.Where(p => p.Content.Contains(key));
                    List<DreamPublication> tempList = new List<DreamPublication>();

                    foreach(DreamPublication dream in thisSearch)
                    {
                        if (!result.Contains(dream))
                        {
                            tempList.Add(dream);
                        }
                    }

                    result.AddRange(tempList);
                }

                ViewBag.Content = result;
                return View("User");
            }

            List<DreamPublication> publications = new List<DreamPublication>();
            foreach (DreamPublication dream in content)
            {
                publications.Add(dream);
            }

            ViewBag.Content = publications;
            return View("User");
        }

        //too much params
        [HttpPost]
        public async Task<IActionResult> AddDream(string dreamName, string topics, string hours,
                                          string[] content, bool isPublic, string authorId)
        {
            _dreamService.Name = dreamName;
            _dreamService.Topics = topics;
            _dreamService.Hours = hours;
            _dreamService.Content = content;
            _dreamService.IsPublic = isPublic;
            _dreamService.AuthorId = authorId;

            await _databaseReader.AddDreamAsync(new DreamPublication());

            return RedirectToActionPermanent("User");
        }


        //why not authorized?
        //what is RequestVerificationToken?
        //why not httpDelete?
        [HttpGet]
        public async Task<IActionResult> RemoveDream(string dreamId, string RequestVerificationToken)
        {
            try
            {
                await _databaseReader.DeleteDreamAsync(HttpContext.User.Identity.Name, dreamId);
            }
            catch{}
            
            return RedirectToActionPermanent("User");
        }


        [AllowAnonymous]
        public async Task<IActionResult> Dream(string publicationId)
        {
            //var publication = _dreamsContext.DreamPublications.First(p => p.Id == publicationId || p.Id == publicationId);
            try
            {
                var publication = await _databaseReader.GetDreamByIdAsync(publicationId);
                bool isEditMode = false;
                ViewBag.Publication = publication;

                if ((HttpContext.User.Identity.IsAuthenticated &&
                     publication.UserAccount.UserName == HttpContext.User.Identity.Name))
                {
                    isEditMode = true;
                }
                else if(!publication.IsPublic)
                {
                    throw new UnauthorizedAccessException();
                }

                return View();
            }
            catch
            {
                return StatusCode(404);
            }
        }
    }
}
