using HtmlAgilityPack;
using MVCWebAppTests.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Scraper
{
    public class DuckDuckGoScraper : IScraper
    {
        private List<SearchResult> results;
        private static readonly string siteUrl = "https://duckduckgo.com/html";

        private static readonly string resultGroupNodeClassName = "results";
        private static readonly string resultGroupNodeId = "links";
        private static readonly string resultNodeClassName = "links_main links_deep result__body";
        private static readonly string resultSnippetClassName = "result__snippet";
        private static readonly string resultUrlParamterName = "amp;uddg";

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

        private static string GetResultNodeSnippet(HtmlNode x)
        {
            return x.Descendants("a")?.Where(y => y.Attributes["class"]?.Value == resultSnippetClassName)?.FirstOrDefault().InnerText;
        }

        private static string GetResultNodeLink(HtmlNode x)
        {
            return HttpUtility.ParseQueryString(
                    x.Descendants("h2")?.FirstOrDefault()
                    ?.Descendants("a")?.FirstOrDefault()?.Attributes["href"]?.Value
                )
                .Get(resultUrlParamterName);
        }

        private static string GetResultNodeTitle(HtmlNode x)
        {
            return x.Descendants("h2")?.FirstOrDefault()?.Descendants("a")?.FirstOrDefault()?.InnerText;
        }

        private IEnumerable<HtmlNode> GetResultNodes(HtmlDocument doc)
        {
            var rootNode = doc.DocumentNode;
            var bodyNode = rootNode.Descendants("body").FirstOrDefault();
            var resultGroupNode = bodyNode?.Descendants("div")
                .Where(x => x.Attributes["id"]?.Value == resultGroupNodeId && x.Attributes["class"]?.Value == resultGroupNodeClassName)
                .FirstOrDefault();

            if (resultGroupNode == null)
            {
                throw new ScraperPageFormatException();
            }

            var resultNodes = resultGroupNode.Descendants("div").Where(x => x.Attributes["class"]?.Value == resultNodeClassName);
            return resultNodes;
        }

        private string BuildQueryUrl(string searchTerm)
        {
            string query = siteUrl + "/?q=" + searchTerm;
            return query;
        }


    }
}
