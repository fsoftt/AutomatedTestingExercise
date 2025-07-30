using Business.Entities;
using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;

namespace Tests
{
    internal class SearchResultsPageTests
    {
        private HomePage homePage;
        private ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

        [SetUp]
        public void Setup()
        {
            homePage = new HomePage(serviceProvider);
        }

        [TearDown]
        public void Teardown()
        {
            BrowserFactory.CloseDriver();
        }

        /*
            Test case #2. Validate global search works as expected.
            Navigate to https://www.epam.com/
            Find a magnifier icon and click on it
            Find a search string and put there “BLOCKCHAIN”/”Cloud”/”Automation” (use as a parameter for a test)
            Click “Find” button
            Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used. 
        */
        [TestCase("Cloud")]
        [TestCase("Blockchain")]
        [TestCase("Automation")]
        public void ValidateGlobalSearch(string searchTerm)
        {
            ResultsPage resultsPage = homePage.Search(searchTerm);

            IEnumerable<Article> articles = resultsPage.GetArticles();

            foreach (Article article in articles)
            {
                Assert.That(article.Title.ToLower().Contains(searchTerm.ToLower())
                    || article.Description.ToLower().Contains(searchTerm.ToLower()), Is.True,
                    $"Article '{article.Title}' does not contain the search term '{searchTerm}'.");
            }
        }
    }
}
