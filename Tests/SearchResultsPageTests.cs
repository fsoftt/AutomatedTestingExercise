using Business.Entities;
using Business.PageObjects;

namespace Tests
{
    internal class SearchResultsPageTests : BaseTest
    {
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
        [Ignore("Testing")]
        [Category("UI")]
        public void ValidateGlobalSearch(string searchTerm)
        {
            ResultsPage resultsPage = homePage.Search(searchTerm);

            IEnumerable<Article> articles = resultsPage.GetArticles();

            Assert.Multiple(() =>
            {
                Assert.That(articles, Is.Not.Empty, "No articles were found in the search results.");

                foreach (Article article in articles)
                {
                    Assert.That(article.Title.ToLower().Contains(searchTerm.ToLower())
                        || article.Description.ToLower().Contains(searchTerm.ToLower()), Is.True,
                        $"Article '{article.Title}' does not contain the search term '{searchTerm}'.");
                }
            });
        }
    }
}
