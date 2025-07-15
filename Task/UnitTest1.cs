using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using Task.Entities;
using Task.Pages;

namespace Task
{
    public class UnitTest1
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        /*
         * Test case #1. Validate that the user can search for a position based on criteria.
            Navigate to https://www.epam.com/
            Find a link “Carriers” and click on it
            Write the name of any programming language in the field “Keywords” (should be taken from test parameter)
            Select “All Locations” in the “Location” field (should be taken from the test parameter)
            Select the option “Remote”
            Click on the button “Find”
            Find the latest element in the list of results
            Click on the button “View and apply”
            Validate that the programming language mentioned in the step above is on a page
        */
        [Test]
        public void SearchForPositionBasedOnCriteria()
        {
            try
            {
                driver = PrepareSite();

                CareersPage careersPage = new HomePage(driver).OpenCareers();
                careersPage.Search(Constants.SearchForPositionBasedOnCriteria.ProgrammingLanguage, isRemote: true);

                PositionPage positionPage = careersPage.ApplyToLatestElement();

                Assert.That(positionPage, Is.Not.Null, "Position page should not be null.");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /*
            Test case #2. Validate global search works as expected.
            Navigate to https://www.epam.com/
            Find a magnifier icon and click on it
            Find a search string and put there “BLOCKCHAIN”/”Cloud”/”Automation” (use as a parameter for a test)
            Click “Find” button
            Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used. 
        */

        [Test]
        public void ValidateGlobalSearch()
        {
            try
            {
                driver = PrepareSite();

                HomePage homePage = new HomePage(driver);
                ResultsPage resultsPage = homePage.Search(Constants.ValidateGlobalSearch.SearchTerm);

                IEnumerable<Article> articles = resultsPage.GetArticles();

                foreach (Article article in articles)
                {
                    Assert.That(article.Title.ToLower().Contains(Constants.ValidateGlobalSearch.SearchTerm.ToLower()) 
                        || article.Description.ToLower().Contains(Constants.ValidateGlobalSearch.SearchTerm.ToLower()), Is.True,
                        $"Article '{article.Title}' does not contain the search term '{Constants.ValidateGlobalSearch.SearchTerm}'.");
                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        private void CheckArticles()
        {
            WaitFor(By.XPath(Constants.ValidateGlobalSearch.Articles));

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath(Constants.ValidateGlobalSearch.Articles));
            foreach (IWebElement element in elements)
            {
                Assert.IsTrue(element.Text.ToLower().Contains(Constants.ValidateGlobalSearch.SearchTerm.ToLower()),
                    $"Element text '{element.Text}' does not contain the search term '{Constants.ValidateGlobalSearch.SearchTerm}'.");
            }
        }

        private IWebDriver PrepareSite()
        {
            driver.Navigate().GoToUrl(Constants.Url);
            driver.Manage().Window.Maximize();

            return driver;
        }

        private void WaitFor(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => driver.FindElement(by).Displayed);
        }

        private void Click(By by)
        {
            driver.FindElement(by)
                .Click();
        }
    }
}