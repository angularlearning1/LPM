using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LPM.Web.Context;
using LPM.Web.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LPM.Web.Controllers
{
    public class UserPicturesController : BaseController
    {
        private readonly LPMContext _context;

        public UserPicturesController(LPMContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.UserPicture.Where(x => x.UserId == UserId).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProfilePicture,IsDefault")] UserPicture userPicture, IFormFile ProfilePicture)
        {
            string sImagePath = GetImagePath(UserId);
            string sImageThumbnailPath = GetThumbnailImagePath(UserId);



            var path = Path.Combine(sImagePath, ProfilePicture.FileName);
            var thumbpath = Path.Combine(sImageThumbnailPath, ProfilePicture.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await ProfilePicture.CopyToAsync(stream);
            }

            Crop(200, 200, ProfilePicture.OpenReadStream(), thumbpath);



            userPicture.UserId = UserId;
            userPicture.ProfilePicture = ProfilePicture.FileName;
            _context.Add(userPicture);

            if (ProfilePicture == null || ProfilePicture.Length == 0)
                return Content("file not selected");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPicture = await _context.UserPicture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPicture == null)
            {
                return NotFound();
            }

            return View(userPicture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userPicture = await _context.UserPicture.FindAsync(id);
            _context.UserPicture.Remove(userPicture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPictureExists(int id)
        {
            return _context.UserPicture.Any(e => e.Id == id);
        }
    }
}
