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
        public abstract string SiteName { get; }
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

        private List<SearchResult> GetResults(HtmlDocument doc)
        {
            IEnumerable<HtmlNode> resultNodes = GetResultNodes(doc);
            List<SearchResult> results = ParseResultNodes(resultNodes);
            return results;
        }

        private List<SearchResult> ParseResultNodes(IEnumerable<HtmlNode> resultNodes)
        {
            var results = resultNodes.Select(x => new SearchResult()
            {
                Title = GetResultNodeTitle(x),
                Link = GetResultNodeLink(x),
                Summary = GetResultNodeSnippet(x)
            }).ToList();
            return results;
        }

        protected abstract string BuildQueryUrl(string searchTerm);
        protected abstract IEnumerable<HtmlNode> GetResultNodes(HtmlDocument doc);
        protected abstract string GetResultNodeTitle(HtmlNode resultNode);
        protected abstract string GetResultNodeLink(HtmlNode resultNode);
        protected abstract string GetResultNodeSnippet(HtmlNode resultNode);

    }
}
