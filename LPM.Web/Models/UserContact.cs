using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPM.Web.Models
{
    public partial class UserContact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Required]
        public string Relation{ get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string ContactAddress { get; set; }
    }
}
