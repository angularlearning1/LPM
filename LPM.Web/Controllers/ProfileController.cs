﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LPM.Web.Context;
using LPM.Web.Models;
using LPM.Web.Util;
using LPM.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LPM.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly LPMContext _context;

        public ProfileController(LPMContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id = 0)
        {
          
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = _context.User.Where(x => x.Id == id).First();
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

            _context.UserVisit.Add(new UserVisit() { ProfileId = id, UserId = UserId, VisitedDate = DateTime.Now });
            _context.SaveChanges();
            return View(profileViewModel);
        }


        
        public IActionResult Edit()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = _context.User.Where(x => x.Id == UserId).First();
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



            ViewBag.ZodiacSigns = Utillites.GetZodiacSigns();
            ViewBag.ColorComplections = Utillites.GetColorComplections();
            ViewBag.BloodGroups = Utillites.GetBloodGroups();
            ViewBag.MaritalStatuses = Utillites.GetMaritalStatuses();
            return View(profileViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.User.Where(x => x.Id == UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Name = profileViewModel.User.Name;
                    user.PhoneNumber = profileViewModel.User.PhoneNumber;
                    user.DateOfBirth = profileViewModel.User.DateOfBirth;
                    user.EmailAddress = profileViewModel.User.EmailAddress;
                }
                var userDetail = _context.UserDetail.Where(X => X.UserId == UserId).FirstOrDefault();
                if (userDetail != null)
                {
                    userDetail.AboutMe = profileViewModel.UserDetail.AboutMe;
                    userDetail.BirthName = profileViewModel.UserDetail.BirthName;
                    userDetail.BloodGroup = profileViewModel.UserDetail.BloodGroup;
                    userDetail.BrithPlace = profileViewModel.UserDetail.BrithPlace;
                    userDetail.BrithTime = profileViewModel.UserDetail.BrithTime;
                    userDetail.ColorComplection = profileViewModel.UserDetail.ColorComplection;
                    userDetail.Gotra = profileViewModel.UserDetail.Gotra;
                    userDetail.Height = profileViewModel.UserDetail.Height;
                    userDetail.Mamkul = profileViewModel.UserDetail.Mamkul;
                    userDetail.MaritalStatus = profileViewModel.UserDetail.MaritalStatus;
                    userDetail.ZodiacSign = profileViewModel.UserDetail.ZodiacSign;
                }

                var UserExpectation = _context.UserExpectation.Where(X => X.UserId == UserId).FirstOrDefault();
                if (UserExpectation != null)
                {
                    UserExpectation.Education = profileViewModel.UserExpectation.Education;
                    UserExpectation.Height = profileViewModel.UserExpectation.Height;
                    UserExpectation.ColorComplection = profileViewModel.UserExpectation.ColorComplection;
                }

                var UserProfession = _context.UserProfession.Where(X => X.UserId == UserId).FirstOrDefault();
                if (UserProfession != null)
                {
                    UserProfession.Education = profileViewModel.UserProfession.Education;
                    UserProfession.AnnualIncome = profileViewModel.UserProfession.AnnualIncome;
                    UserProfession.Profession = profileViewModel.UserProfession.Profession;
                    UserProfession.Workplace = profileViewModel.UserProfession.Workplace;
                }
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Edit));
        }

    }
}