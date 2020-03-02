using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPM.Web.Models
{
    public partial class UserExpectation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
      
        [StringLength(250)]
    
        public string Education { get; set; }
      
        [StringLength(50)]
        [Display(Name = "Color Complection")]
        public string ColorComplection { get; set; }
      
        [StringLength(50)]
        
        public string Height { get; set; }

      
    }
}
