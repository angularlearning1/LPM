using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using LPM.Web.Context;
using LPM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LPM.Web.Controllers
{

    [Authorize(Policy = "ElevatedRights")]
    public class BaseController : Controller
    {
       
        public BaseController()
        {
           
        }

        protected Int32 UserId => Convert.ToInt32(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);
        protected string UserEmailAddress => Convert.ToString(User.Claims.Where(x => x.Type == ClaimTypes.Email).First().Value);


        protected string GetImagePath(int intUserId)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", intUserId.ToString());
            CreateDirectory(path);
            return path;
        }

        protected string GetThumbnailImagePath(int intUserId)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", intUserId.ToString(), "thumbnail");
            CreateDirectory(path);
            return path;
        }

        protected string GetImageProfilePath(UserPicture userPicture, int gender)
        {
            if (!System.IO.File.Exists(string.Format(@"..\img\{0}\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture)))
            {
                if (gender == 1)
                    return @"..\img\male.jpg";
                else
                    return @"..\img\female.jpg";
            }
            return string.Format(@"..\img\{0}\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
        }
        protected string GetImageProfileThumbnailPath(UserPicture userPicture, int gender)
        {
            if (!System.IO.File.Exists(string.Format(@"..\img\{0}\thumbnail\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture)))
            {
                if (gender == 1)
                    return @"..\img\male_thumb.jpg";
                else
                    return @"..\img\female_thumb.jpg";
            }
                
            return string.Format(@"..\img\{0}\thumbnail\{1}", userPicture.UserId.ToString(), userPicture.ProfilePicture);
        }

        protected void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void Crop(int Width, int Height, Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new Bitmap(streamImg);
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping   
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                    // Save the file path, note we use png format to support png file   
                    objBitmap.Save(saveFilePath);
                }
            }
        }
    }
}
