using System.Collections.Generic;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Models
{
    public class SiteSearchResults
    {
        public List<SearchResult> SearchResults { get; set; }
        public string SiteName { get; set; }
    }
}
