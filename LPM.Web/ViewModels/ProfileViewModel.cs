using LPM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPM.Web.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public UserContact UserContact { get; set; }
        public List<UserContact> UserContacts { get; set; }
        public UserDetail UserDetail { get; set; }

        public UserProfession UserProfession { get; set; }
        public UserExpectation UserExpectation { get; set; }

        public UserPicture UserPicture { get; set; }
        public List<UserPicture> UserPictures { get; set; }
    }
}
