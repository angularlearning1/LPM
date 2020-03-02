using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPM.Web.Context;
using LPM.Web.Models;
using LPM.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LPM.Web.Util;

namespace LPM.Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly LPMContext _context;

        public SearchController(LPMContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ProfileViewModel profileViewModel = null;
            List<ProfileViewModel> profileViewModels = new List<ProfileViewModel>();
            foreach (var item in _context.User.Where(x => x.Id != UserId).ToList())
            {
                string sImagePath = GetImagePath(UserId);
                string sThumbImagePath = GetThumbnailImagePath(UserId);
                profileViewModel = new ProfileViewModel();

                profileViewModel.User = _context.User.Where(x => x.Id == item.Id).First();
                profileViewModel.UserContact = new UserContact();
                profileViewModel.UserContacts = _context.UserContact.Where(x => x.UserId == profileViewModel.User.Id).ToList();
                profileViewModel.UserDetail = _context.UserDetail.Where(x => x.UserId == profileViewModel.User.Id).FirstOrDefault();
                if (profileViewModel.UserDetail == null)
                {
                    profileViewModel.UserDetail = new UserDetail();
                }
                profileViewModel.UserProfession = _context.UserProfession.Where(x => x.UserId == profileViewModel.User.Id).FirstOrDefault();
                if (profileViewModel.UserProfession == null)
                {
                    profileViewModel.UserProfession = new UserProfession();
                }
                profileViewModel.UserExpectation = _context.UserExpectation.Where(x => x.UserId == profileViewModel.User.Id).FirstOrDefault();
                if (profileViewModel.UserExpectation == null)
                {
                    profileViewModel.UserExpectation = new UserExpectation();
                }
                profileViewModel.UserPicture = _context.UserPicture.Where(x => x.UserId == profileViewModel.User.Id && x.IsDefault == true).FirstOrDefault();
                if (profileViewModel.UserPicture == null)
                {
                    profileViewModel.UserPicture = new UserPicture();
                }
                profileViewModel.UserPictures = _context.UserPicture.Where(x => x.UserId == profileViewModel.User.Id && x.IsDefault != true).ToList();

                foreach (var userPicture in profileViewModel.UserPictures)
                {
                    userPicture.ImageProfilePath = GetImageProfilePath(userPicture, profileViewModel.User.Gender);
                    userPicture.ImageProfileThumbnailPath = GetImageProfileThumbnailPath(userPicture, profileViewModel.User.Gender);
                }
                profileViewModel.UserPicture.ImageProfilePath = GetImageProfilePath(profileViewModel.UserPicture, profileViewModel.User.Gender);
                profileViewModel.UserPicture.ImageProfileThumbnailPath = GetImageProfileThumbnailPath(profileViewModel.UserPicture, profileViewModel.User.Gender);

                profileViewModels.Add(profileViewModel);
            }
            ViewBag.OrderBy = Utillites.GetSearchOrders();
            ViewBag.Gender = Utillites.GetGenders();
            return View(profileViewModels);
        }
        [HttpPost]
        public IActionResult Index(string Name, string OrderBy, int fromAge, int toAge, int Gender)
        {
            List<UserSearchViewModel> userSearchViewModels = new List<UserSearchViewModel>();
            UserSearchViewModel userSearchViewModel = null;
            var userList = _context.User.Where(x => x.Id != UserId && x.Active == true).ToList();
            foreach (var item in userList)
            {
                userSearchViewModel = new UserSearchViewModel();
                userSearchViewModel.Id = item.Id;
                userSearchViewModel.Gender = item.Gender;
                userSearchViewModel.Name = item.Name;
                userSearchViewModel.Age = (new DateTime(DateTime.Now.Subtract(item.DateOfBirth).Ticks).Year - 1);
                if (_context.UserLogin.Where(x => x.UserId == item.Id).Any())
                {
                    userSearchViewModel.LoginDate = _context.UserLogin.Where(x => x.UserId == item.Id).Max(x => x.LoginDate);
                }
                userSearchViewModels.Add(userSearchViewModel);
            }


            if (Gender != 0)
            {
                userSearchViewModels = userSearchViewModels.Where(x => x.Gender == Gender).ToList();
            }
            if (fromAge != 0 && toAge != 0)
            {
                userSearchViewModels = userSearchViewModels.Where(x => x.Age >= fromAge && x.Age <= toAge).ToList();
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                userSearchViewModels = userSearchViewModels.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
            }

            if (OrderBy == "1")
            {
                userSearchViewModels = userSearchViewModels.OrderBy(x => x.Name).ToList();
            }
            if (OrderBy == "2")
            {
                userSearchViewModels = userSearchViewModels.OrderBy(x => x.Age).ToList();
            }
            if (OrderBy == "3")
            {
                userSearchViewModels = userSearchViewModels.OrderByDescending(x => x.LoginDate).ToList();
            }
            ProfileViewModel profileViewModel = null;
            List<ProfileViewModel> profileViewModels = new List<ProfileViewModel>();



            foreach (var item in userSearchViewModels)
            {
                string sImagePath = GetImagePath(UserId);
                string sThumbImagePath = GetThumbnailImagePath(UserId);
                profileViewModel = new ProfileViewModel();

                profileViewModel.User = _context.User.Where(x => x.Id == item.Id).First();



                profileViewModel.UserContact = new UserContact();
                profileViewModel.UserContacts = _context.UserContact.Where(x => x.UserId == profileViewModel.User.Id).ToList();
                profileViewModel.UserDetail = _context.UserDetail.Where(x => x.UserId == profileViewModel.User.Id).FirstOrDefault();
                if (profileViewModel.UserDetail == null)
                {
                    profileViewModel.UserDetail = new UserDetail();
                }
                profileViewModel.UserProfession = _context.UserProfession.Where(x => x.UserId == profileViewModel.User.Id).FirstOrDefault();
                if (profileViewModel.UserProfession == null)
                {
                    profileViewModel.UserProfession = new UserProfession();
                }
                profileViewModel.UserExpectation = _context.UserExpectation.Where(x => x.UserId == profileViewModel.User.Id).FirstOrDefault();
                if (profileViewModel.UserExpectation == null)
                {
                    profileViewModel.UserExpectation = new UserExpectation();
                }
                profileViewModel.UserPicture = _context.UserPicture.Where(x => x.UserId == profileViewModel.User.Id && x.IsDefault == true).FirstOrDefault();
                if (profileViewModel.UserPicture == null)
                {
                    profileViewModel.UserPicture = new UserPicture();
                }
                profileViewModel.UserPictures = _context.UserPicture.Where(x => x.UserId == profileViewModel.User.Id && x.IsDefault != true).ToList();

                foreach (var userPicture in profileViewModel.UserPictures)
                {
                    userPicture.ImageProfilePath = GetImageProfilePath(userPicture, profileViewModel.User.Gender);
                    userPicture.ImageProfileThumbnailPath = GetImageProfileThumbnailPath(userPicture, profileViewModel.User.Gender);
                }
                profileViewModel.UserPicture.ImageProfilePath = GetImageProfilePath(profileViewModel.UserPicture, profileViewModel.User.Gender);
                profileViewModel.UserPicture.ImageProfileThumbnailPath = GetImageProfileThumbnailPath(profileViewModel.UserPicture, profileViewModel.User.Gender);


                profileViewModels.Add(profileViewModel);
            }
            ViewBag.OrderBy = Utillites.GetSearchOrders();
            ViewBag.Gender = Utillites.GetGenders();
            return PartialView("_Search", profileViewModels);
        }

    }
}