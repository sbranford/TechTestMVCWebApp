using System;
using System.Collections.Generic;
using System.Text;
using TechTestMVCWebApp.Scraper;

namespace MVCWebAppTests.Scraper
{
    class DuckDuckGoScraperTests : ScraperTests
    {
        protected override IScraper CreateScraper()
        {
            return new DuckDuckGoScraper();
        }
    }
}
