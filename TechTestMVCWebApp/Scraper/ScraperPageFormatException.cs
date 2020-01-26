using System;
using System.Collections.Generic;
using System.Text;

namespace TechTestMVCWebApp.Scraper
{
    public class ScraperPageFormatException : Exception
    {
        public ScraperPageFormatException() : base("Page did not have the expected format")
        {

        }
    }
}
