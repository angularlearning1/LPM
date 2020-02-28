using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPM.Web.Util
{
    public class EnumModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ListNameValueModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public static class Utillites
    {
        public static List<SelectListItem> GetZodiacSigns()
        {
            List<SelectListItem> ListItemValueModels = new List<SelectListItem>();
            ListItemValueModels.Add(new SelectListItem() { Text = "--Zodiac Sign--", Value = "" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Aries", Value = "Aries" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Taurus", Value = "Taurus" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Gemini", Value = "Gemini" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Cancer", Value = "Cancer" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Leo", Value = "Leo" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Virgo", Value = "Virgo" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Libra", Value = "Libra" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Scorpio", Value = "Scorpio" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Sagittarius", Value = "Sagittarius" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Capricorn", Value = "Capricorn" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Aquarius", Value = "Aquarius" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Pisces", Value = "Pisces" });
            return ListItemValueModels;
        }

        public static List<SelectListItem> GetColorComplexions()
        {
            List<SelectListItem> ListItemValueModels = new List<SelectListItem>();
            ListItemValueModels.Add(new SelectListItem() { Text = "--Color Complexion--", Value = "" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Fair"  , Value = "Fair"   });
            ListItemValueModels.Add(new SelectListItem() { Text = "Medium", Value = "Medium" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Olive" , Value = "Olive"  });
            ListItemValueModels.Add(new SelectListItem() { Text = "Brown", Value = "Brown"  });
            return ListItemValueModels;
        }

        public static List<SelectListItem> GetBloodGroups()
        {
            List<SelectListItem> ListItemValueModels = new List<SelectListItem>();
            ListItemValueModels.Add(new SelectListItem() { Text = "--Blood Group--", Value = "" });
            ListItemValueModels.Add(new SelectListItem() { Text = "A+", Value = "A+"  });
            ListItemValueModels.Add(new SelectListItem() { Text = "B+" , Value = "B+"  });
            ListItemValueModels.Add(new SelectListItem() { Text = "AB+", Value = "AB+" });
            ListItemValueModels.Add(new SelectListItem() { Text = "O+" , Value = "O+"  });
            ListItemValueModels.Add(new SelectListItem() { Text = "A-" , Value = "A-"  });
            ListItemValueModels.Add(new SelectListItem() { Text = "B-" , Value = "B-"  });
            ListItemValueModels.Add(new SelectListItem() { Text = "AB-", Value = "AB-" });
            ListItemValueModels.Add(new SelectListItem() { Text = "O-" , Value = "O-"  });
            return ListItemValueModels;
        }

        public static List<SelectListItem> GetMaritalStatuses()
        {
            List<SelectListItem> ListItemValueModels = new List<SelectListItem>();
            ListItemValueModels.Add(new SelectListItem() { Text = "--Marital Status--", Value = "" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Single" , Value = "Single" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Divorced", Value = "Divorced" });
            return ListItemValueModels;
        }


        public static List<SelectListItem> GetSearchOrders()
        {
            List<SelectListItem> ListItemValueModels = new List<SelectListItem>();
            ListItemValueModels.Add(new SelectListItem() { Text = "--Order By--", Value = "0" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Name", Value = "1" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Age", Value = "2" });
            ListItemValueModels.Add(new SelectListItem() { Text = "Login Time", Value = "3" });
            return ListItemValueModels;
        }
       public enum SearchOrderEnum
        {
            Name = 1,
            Age = 2,
            LoginTime = 3,
        }
    }

   
}
