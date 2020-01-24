using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Scraper
{
    public abstract class AbstractScraper : IScraper
    {
        public List<SearchResult> More()
        {
            throw new NotImplementedException();
        }

        public List<SearchResult> Scrape(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
