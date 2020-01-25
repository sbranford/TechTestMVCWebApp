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
    public class DuckDuckGoScraper : AbstractScraper
    {
        private static readonly string resultGroupNodeClassName = "results";
        private static readonly string resultGroupNodeId = "links";
        private static readonly string resultNodeClassName = "links_main links_deep result__body";
        private static readonly string resultSnippetClassName = "result__snippet";
        private static readonly string resultUrlParamterName = "amp;uddg";

        protected override string BuildQueryUrl(string searchTerm)
        {
            var siteUrl = "https://duckduckgo.com/html/";
            string query = siteUrl + "?q=" + searchTerm;
            return query;
        }

        protected override IEnumerable<HtmlNode> GetResultNodes(HtmlDocument doc)
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

        protected override string GetResultNodeSnippet(HtmlNode x)
        {
            return x.Descendants("a")?.Where(y => y.Attributes["class"]?.Value == resultSnippetClassName)?.FirstOrDefault()?.InnerText;
        }

        protected override string GetResultNodeLink(HtmlNode x)
        {
            return HttpUtility.ParseQueryString(
                    x.Descendants("h2")?.FirstOrDefault()
                    ?.Descendants("a")?.FirstOrDefault()?.Attributes["href"]?.Value
                )
                .Get(resultUrlParamterName);
        }

        protected override string GetResultNodeTitle(HtmlNode x)
        {
            return x.Descendants("h2")?.FirstOrDefault()?.Descendants("a")?.FirstOrDefault()?.InnerText;
        }
    }
}
