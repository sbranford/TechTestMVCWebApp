using NUnit.Framework;
using System.Linq;
using TechTestMVCWebApp.Scraper;

namespace MVCWebAppTests.Scraper
{
    [TestFixture]
    public abstract class ScraperTests
    {
        private IScraper scraper;

        [SetUp]
        public void Setup()
        {
            scraper = CreateScraper();
        }

        protected abstract IScraper CreateScraper();

        [Test]
        public void Scrape_ReturnsEmptyList_WhenSearchTermInput()
        {
            //Arrange
            string searchTerm = "";

            //Act
            var results = scraper.Scrape(searchTerm);

            //Assert
            Assert.IsEmpty(results);
        }

        [Test]
        public void Scrape_ReturnsNonEmptyList_WithValidSearchTerm()
        {
            //Arrange
            string searchTerm = "duck";

            //Act
            var results = scraper.Scrape(searchTerm);

            //Assert
            Assert.IsNotEmpty(results);
        }

        [Test]
        public void Scrape_AllResultsHaveLinks_WithValidSearchTerm()
        {
            //Arrange
            string searchTerm = "duck";

            //Act
            var results = scraper.Scrape(searchTerm);
            var nullLinkResults = results.Where(x => x.Link == null).ToList();

            //Assert
            Assert.IsEmpty(nullLinkResults);
            
        }
    }
}