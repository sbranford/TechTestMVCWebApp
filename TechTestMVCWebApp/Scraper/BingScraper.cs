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
    public class BingScraper : AbstractScraper
    {
        private static readonly string resultGroupNodeId = "b_results";
        private static readonly string resultNodeClassName = "b_algo";
        private static readonly string resultSnippetClassName = "b_caption";
        private static readonly string resultUrlParamterName = "amp;uddg";
        public BingScraper ()
        {
            siteUrl = "https://www.bing.com/search";
        }
        protected override IEnumerable<HtmlNode> GetResultNodes(HtmlDocument doc)
        {
            var rootNode = doc.DocumentNode;
            var bodyNode = rootNode.Descendants("body").FirstOrDefault();
            var resultGroupNode = bodyNode?.Descendants("ol")
                .Where(x => x.Attributes["id"]?.Value == resultGroupNodeId)
                .FirstOrDefault();

            if (resultGroupNode == null)
            {
                throw new ScraperPageFormatException();
            }

            var resultNodes = resultGroupNode.Descendants("li").Where(x => x.Attributes["class"]?.Value == resultNodeClassName);
            return resultNodes;
        }

        protected override string GetResultNodeSnippet(HtmlNode x)
        {
            var snippet = x.Descendants("div")
                ?.Where(y => y.Attributes["class"]?.Value == resultSnippetClassName)?.FirstOrDefault()
                ?.Descendants("p")?.FirstOrDefault()?.InnerText;
            return snippet;
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
