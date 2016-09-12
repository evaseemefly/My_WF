using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lshOA.UI.Models
{
    public class SearchResultViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentDescription { get; set; }
        public string Url { get; set; }
    }
}