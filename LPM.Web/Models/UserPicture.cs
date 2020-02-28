using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPM.Web.Models
{
    public partial class UserPicture
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsDefault { get; set; }
        [NotMapped]
        public string ImageProfileThumbnailPath { get; set; }
        [NotMapped]
        public string ImageProfilePath { get; set; }
    }
}
