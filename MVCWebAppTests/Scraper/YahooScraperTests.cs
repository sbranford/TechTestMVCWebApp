using System;
using System.Collections.Generic;
using System.Text;
using TechTestMVCWebApp.Scraper;

namespace MVCWebAppTests.Scraper
{
    namespace MVCWebAppTests.Scraper
    {
        class YahooScraperTests : ScraperTests
        {
            protected override IScraper CreateScraper()
            {
                return new YahooScraper();
            }
        }
    }
}
