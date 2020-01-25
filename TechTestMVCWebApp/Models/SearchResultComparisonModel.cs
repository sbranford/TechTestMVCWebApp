using System;
using System.Linq;
using System.Threading.Tasks;

namespace TechTestMVCWebApp.Models
{ 
    public class SearchResultComparisonModel
    {
        public SiteSearchResults SiteAResults { get; set; }
        public SiteSearchResults SiteBResults { get; set; }
    }
}
