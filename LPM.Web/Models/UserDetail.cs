using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPM.Web.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AboutMe { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Brith Place")]
        public string BrithPlace { get; set; }
        [Required]
        [Display(Name = "Brith Time")]
        [DataType(DataType.DateTime)]
        public DateTime BrithTime { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Birth Name")]
        public string BirthName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Zodiac Sign")]
        public string ZodiacSign { get; set; }
        [Required]
        [StringLength(50)]
        public string Gotra { get; set; }
        [Required]
        [StringLength(50)]
        public string Mamkul { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Color Complection")]
        public string ColorComplection { get; set; }
        [Required]
        [StringLength(50)]
        public string Height { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }


       
    }
}
