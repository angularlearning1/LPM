using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPM.Web.Context;
using LPM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using static LPM.Web.Util.Utillites;

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
                    userPicture.ImageProfilePath = string.Format(@"..\img\{0}\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
                    userPicture.ImageProfileThumbnailPath = string.Format(@"..\img\{0}\thumbnail\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
                }
                profileViewModel.UserPicture.ImageProfilePath = string.Format(@"..\img\{0}\{1}", profileViewModel.UserPicture.UserId.ToString(), profileViewModel.UserPicture.ProfilePicture);
                profileViewModel.UserPicture.ImageProfileThumbnailPath = string.Format(@"..\img\{0}\thumbnail\{1}", profileViewModel.UserPicture.UserId.ToString(), profileViewModel.UserPicture.ProfilePicture);


                profileViewModels.Add(profileViewModel);
            }

            return View(profileViewModels);
        }
        [HttpPost]
        public IActionResult Index(SearchViewModel searchViewModel)
        {
            var UserList = (from u in _context.User
                            join l in _context.UserLogin on u.Id equals l.UserId
                            select new
                            {
                                Id = u.Id,
                                l.LoginDate,
                                u.Gender,
                                u.DateOfBirth,
                                u.Name,
                                Age = (new DateTime(DateTime.Now.Subtract(u.DateOfBirth).Ticks).Year - 1)
                            }).ToList();
            if (searchViewModel != null)
            {
                if (searchViewModel.FromAge != 0 && searchViewModel.ToAge != 0)
                {
                    UserList = UserList.Where(x => x.Age >= searchViewModel.FromAge && x.Age <= searchViewModel.ToAge).ToList();
                }
                if (searchViewModel.Gender != 0)
                {
                    UserList = UserList.Where(x => x.Gender == searchViewModel.Gender).ToList();
                }

                if (!string.IsNullOrWhiteSpace(searchViewModel.Name))
                {
                    UserList = UserList.Where(x => x.Name.Contains(searchViewModel.Name)).ToList();
                }

                if (searchViewModel.OrderBy == (int)SearchOrderEnum.Name)
                {
                    UserList = UserList.Where(x => x.Name.Contains(searchViewModel.Name)).OrderBy(x => x.Name).ToList();
                }
                if (searchViewModel.OrderBy == (int)SearchOrderEnum.Age)
                {
                    UserList = UserList.Where(x => x.Name.Contains(searchViewModel.Name)).OrderBy(x => x.Age).ToList();
                }
                if (searchViewModel.OrderBy == (int)SearchOrderEnum.LoginTime)
                {
                    UserList = UserList.Where(x => x.Name.Contains(searchViewModel.Name)).OrderByDescending(x => x.LoginDate).ToList();
                }
            }
            ProfileViewModel profileViewModel = null;
            List<ProfileViewModel> profileViewModels = new List<ProfileViewModel>();



            foreach (var item in UserList.Where(x => x.Id != UserId).ToList())
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
                    userPicture.ImageProfilePath = string.Format(@"..\img\{0}\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
                    userPicture.ImageProfileThumbnailPath = string.Format(@"..\img\{0}\thumbnail\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
                }
                profileViewModel.UserPicture.ImageProfilePath = string.Format(@"..\img\{0}\{1}", profileViewModel.UserPicture.UserId.ToString(), profileViewModel.UserPicture.ProfilePicture);
                profileViewModel.UserPicture.ImageProfileThumbnailPath = string.Format(@"..\img\{0}\thumbnail\{1}", profileViewModel.UserPicture.UserId.ToString(), profileViewModel.UserPicture.ProfilePicture);


                profileViewModels.Add(profileViewModel);
            }

            return View(profileViewModels);
        }


        [HttpGet("{stext}")]
        public IActionResult Search(string stext)
        {
            var UserList = (from u in _context.User
                            join l in _context.UserLogin on u.Id equals l.UserId
                            select new
                            {
                                Id = u.Id,
                                l.LoginDate,
                                u.Gender,
                                u.DateOfBirth,
                                u.Name,
                                Age = (new DateTime(DateTime.Now.Subtract(u.DateOfBirth).Ticks).Year - 1)

                            }).ToList();
            if (stext != "")
            {
                UserList = UserList.Where(x => x.Name.Contains(stext) || x.Age.ToString().Contains(stext)).ToList();
            }
            ProfileViewModel profileViewModel = null;
            List<ProfileViewModel> profileViewModels = new List<ProfileViewModel>();



            foreach (var item in UserList.Where(x => x.Id != UserId).ToList())
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
                    userPicture.ImageProfilePath = string.Format(@"..\img\{0}\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
                    userPicture.ImageProfileThumbnailPath = string.Format(@"..\img\{0}\thumbnail\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
                }
                profileViewModel.UserPicture.ImageProfilePath = string.Format(@"..\img\{0}\{1}", profileViewModel.UserPicture.UserId.ToString(), profileViewModel.UserPicture.ProfilePicture);
                profileViewModel.UserPicture.ImageProfileThumbnailPath = string.Format(@"..\img\{0}\thumbnail\{1}", profileViewModel.UserPicture.UserId.ToString(), profileViewModel.UserPicture.ProfilePicture);


                profileViewModels.Add(profileViewModel);
            }

            return View(profileViewModels);
        }
    }
}