using System;
using System.Collections.Generic;

namespace LPM.Web.Models
{
    public partial class UserVisit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public DateTime VisitedDate { get; set; }
    }
}
