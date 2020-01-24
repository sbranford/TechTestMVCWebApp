using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Scraper
{
    public abstract class AbstractScraper : IScraper
    {
        protected List<SearchResult> results;
        protected string siteUrl;
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
            string queryUrl = BuildQueryUrl(searchTerm);
            var doc = web.Load(queryUrl);
            results = GetResults(doc);
            return results;
        }
        private string BuildQueryUrl(string searchTerm)
        {
            string query = siteUrl + "/?q=" + searchTerm;
            return query;
        }

        private List<SearchResult> GetResults(HtmlDocument doc)
        {
            IEnumerable<HtmlNode> resultNodes = GetResultNodes(doc);
            List<SearchResult> results = ParseResultNodes(resultNodes);
            return results;
        }

        protected abstract IEnumerable<HtmlNode> GetResultNodes(HtmlDocument doc);
        protected abstract List<SearchResult> ParseResultNodes(IEnumerable<HtmlNode> resultNodes);
    }
}
