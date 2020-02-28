using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPM.Web.Models
{
    public partial class UserProfession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
    
        public string Education { get; set; }
        [Required]
        [StringLength(250)]
    
        public string Profession { get; set; }
        [Required]
        [StringLength(250)]

        public string Workplace { get; set; }
        [Required]
        [Display(Name = "Annual Income")]
        public decimal AnnualIncome { get; set; }
    }
}
