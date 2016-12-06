using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinnSports.WEB.Models
{
    public class SportResultsViewModel
    {
        public string SportName
        {
            get; set;
        }

        public IList<ResultViewModel> Events
        {
            get; set;
        }       
    }
}