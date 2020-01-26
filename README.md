# TechTestMVCWebApp
Repo for the Blackdot Solutions Technical Test

Notes on the project:
* Firstly, I interpreted the brief's instructions not to use search engine APIs to mean "Do Web Scraping"
* I decided to use an ASP.Net Core MVC template as this would allow me to create a basic UI quicker than the blank template
* After some research I opted to use the HTML agility pack library to perform the scraping
* I added an NUnit test project as I wanted to practice TDD
* I decided to create one scraper class for each search engine, and that as each would perform the same basic task in a slightly different way, to use the template pattern and make each scraper inherit from an abstract class that implemented the common elements
* The UI compares the first page of results for two search engines side-by-side

**To run**: either use command "dotnet run -p TechTestMVCWebApp" and navigate to https://localhost:5001/ (you will be warned the site is not secure), or run from within Visual Studio
