using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Scraper
{
    public interface IScraper
    {
        public List<SearchResult> Scrape(string searchTerm);
        public string SiteName { get; }
    }
}
