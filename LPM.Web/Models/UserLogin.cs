using System;
using System.Collections.Generic;

namespace LPM.Web.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
