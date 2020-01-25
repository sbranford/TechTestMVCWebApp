using HtmlAgilityPack;
using MVCWebAppTests.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TechTestMVCWebApp.Models;

namespace TechTestMVCWebApp.Scraper
{
    public class YahooScraper : AbstractScraper
    {
        private static readonly string resultGroupNodeClassName = "mb-15 reg searchCenterMiddle";
        private static readonly string resultSnippetClassName = "compText aAbs";
        public override string SiteName => "Yahoo";
        protected override string BuildQueryUrl(string searchTerm)
        {
            var siteUrl = "https://uk.search.yahoo.com/search";
            string query = $"{siteUrl}?p={searchTerm}";
            return query;
        }
        protected override IEnumerable<HtmlNode> GetResultNodes(HtmlDocument doc)
        {
            var rootNode = doc.DocumentNode;
            var bodyNode = rootNode.Descendants("body").FirstOrDefault();
            var resultGroupNode = bodyNode?.Descendants("ol")
                .Where(x => x.Attributes["class"]?.Value == (resultGroupNodeClassName)).FirstOrDefault();

            if (resultGroupNode == null)
            {
                throw new ScraperPageFormatException();
            }

            var resultNodes = resultGroupNode.ChildNodes.Where(x => x.Name == "li");
            return resultNodes;
        }

        protected override string GetResultNodeSnippet(HtmlNode node)
        {
            var snippet = node.Descendants("div")
                ?.Where(y => y.Attributes["class"]?.Value == resultSnippetClassName)?.FirstOrDefault()
                ?.Descendants("p")?.FirstOrDefault()?.InnerText;
            return snippet;
        }

        protected override string GetResultNodeLink(HtmlNode node)
        {
            string link = node.Descendants("h3")?.FirstOrDefault()
                    ?.Descendants("a")?.FirstOrDefault()?.Attributes["href"]?.Value;
            return link;
        }

        protected override string GetResultNodeTitle(HtmlNode node)
        {
            return node.Descendants("h3")?.FirstOrDefault()?.Descendants("a")?.FirstOrDefault()?.InnerText;
        }
    }
}
