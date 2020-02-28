using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPM.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool bAuthenticate { get; set; }
    }
}
