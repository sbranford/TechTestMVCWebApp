using HtmlAgilityPack;
using MVCWebAppTests.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Scraper
{
    public class DuckDuckGoScraper : IScraper
    {
        private List<SearchResult> results;
        private readonly string siteUrl = "https://duckduckgo.com/html";
        private readonly string resultClassName = "result results_links results_links_deep web-result ";
        public List<SearchResult> More()
        {
            throw new NotImplementedException();
        }

        public List<SearchResult> Scrape(string searchTerm)
        {
            results = new List<SearchResult>();
            if (string.IsNullOrEmpty(searchTerm))
            {
                return results;
            }
            var web = new HtmlWeb();
            web.PreRequest = OnPreRequest;
            string queryUrl = BuildQueryUrl(searchTerm);
            var doc = web.Load(queryUrl);
            var rootNode = doc.DocumentNode;
            var bodyNode = rootNode.Descendants("body").FirstOrDefault();
            var resultsNode = bodyNode?.Descendants("div").Where(x => x.Attributes["class"] == null ? false : x.Attributes["class"].Value == "serp__results").FirstOrDefault();
            var linksNode = resultsNode?.Descendants("div").Where(x => x.Attributes["id"] == null ? false : x.Attributes["id"].Value == "links").FirstOrDefault();

            if (linksNode == null)
            {
                throw new ScraperPageFormatException();
            }
            
            return results;    
        }

        private string BuildQueryUrl (string searchTerm)
        {
            string query = siteUrl + "/?q=" + searchTerm;
            return query;
        }

        private static bool OnPreRequest(HttpWebRequest request)
        {
            request.AllowAutoRedirect = true;
            return true;
        }

    }
}
