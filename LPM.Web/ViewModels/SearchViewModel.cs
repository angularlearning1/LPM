using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPM.Web.ViewModels
{
    public class SearchViewModel
    {
        public string Name { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int Gender { get; set; }
        public int OrderBy { get; set; }
    }

    

}
