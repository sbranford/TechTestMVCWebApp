using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWebAppTests.Scraper;
using TechTestMVCWebApp.Models;
using TechTestMVCWebApp.Scraper;

namespace TechTestMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IScraper scraperA = new DuckDuckGoScraper();
        private IScraper scraperB = new YahooScraper();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string search)
        {           
            ViewBag.SearchTerm = search;
            try
            {
                var searchResultsComparisonModel = BuildSearchResultComparison(search);
                return View(searchResultsComparisonModel);
            }
            catch (ScraperPageFormatException)
            {
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        private SearchResultComparisonModel BuildSearchResultComparison(string search)
        {
            var scraperAResults = scraperA.Scrape(search);
            var scraperBResults = scraperB.Scrape(search);

            var searchResultsComparisonModel = new SearchResultComparisonModel()
            {
                SiteAResults = new SiteSearchResults() { SiteName = scraperA.SiteName, SearchResults = scraperAResults },
                SiteBResults = new SiteSearchResults() { SiteName = scraperB.SiteName, SearchResults = scraperBResults }
            };
            return searchResultsComparisonModel;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
