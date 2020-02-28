using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPM.Web.Models
{
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
      
        public int Gender { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
