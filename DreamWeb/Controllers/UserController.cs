﻿using AutoMapper;
using DreamWeb.DAL;
using DreamWeb.DAL.Entities;
using DreamWeb.DreamPublicationSorting;
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
        private readonly IDatabaseReader _databaseReader;
        private readonly IDreamsSorting _dreamsSorting;
        private readonly IMapper _mapper;

        public UserController(IDatabaseReader databaseReader,
            IDreamsSorting dreamsSorting,
            IMapper mapper)
        {
            _databaseReader = databaseReader;
            _dreamsSorting = dreamsSorting;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> User()
        {
            var user = await _databaseReader.GetUserAccountByUsernameAsync(HttpContext.User.Identity.Name);
            user.Dreams.OrderByDescending(p => p.CreationDate);

            ViewBag.User = user;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DreamsSort(int orderBy, DateTime date, string keyWords)
        {
            var user = await _databaseReader.GetUserAccountByUsernameAsync(HttpContext.User.Identity.Name);
            var dreams = user.Dreams.ToList();

            ViewBag.UserContext = user;
            string[] keys = keyWords.Split(new char[] {' ', ',', '.'});

            List<Dream> result = _dreamsSorting.SortByOrder(dreams, orderBy);

            if (date != DateTime.MinValue)
            {
                result = _dreamsSorting.SortByDate(result, date);
            }

            if (keys.Length > 0)
            {
                result = _dreamsSorting.SortByKeyWords(result, keys);
            }

            ViewBag.Content = result;
            return View("User");
        }

        [HttpPost]
        public async Task<IActionResult> AddDream(DreamInputModel dream)
        {
            Dream dreamToSave = _mapper.Map<Dream>(dream);

            await _databaseReader.AddDreamAsync(dreamToSave, HttpContext.User.Identity.Name);

            return RedirectToActionPermanent("User");
        }

        //what is RequestVerificationToken?
        //why not httpDelete?
        [ValidateAntiForgeryToken]
        [HttpPost]
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
