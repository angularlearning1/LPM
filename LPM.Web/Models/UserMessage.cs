using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPM.Web.Models
{
    public partial class UserMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
    }
}
