using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace Task
{
    public class UnitTest1
    {
        private WebDriver driver;

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
                PrepareSite();
                Click(By.LinkText(Constants.SearchForPositionBasedOnCriteria.CareersLinkText));
                FillPositionForm();
                ApplyToLatestElement();
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
                PrepareSite();
                FillSearchForm();
                CheckArticles();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        private void CheckArticles()
        {
            WaitFor(By.XPath(Constants.ValidateGlobalSearch.LinksContainingTheWords));

            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath(Constants.ValidateGlobalSearch.LinksContainingTheWords));
            foreach (IWebElement element in elements)
            {
                Assert.IsTrue(element.Text.ToLower().Contains(Constants.ValidateGlobalSearch.SearchTerm.ToLower()),
                    $"Element text '{element.Text}' does not contain the search term '{Constants.ValidateGlobalSearch.SearchTerm}'.");
            }
        }

        private void FillSearchForm()
        {
            Click(By.ClassName(Constants.ValidateGlobalSearch.Magnifier));
            WaitFor(By.Name(Constants.ValidateGlobalSearch.Search));

            IWebElement search = driver.FindElement(By.Name(Constants.ValidateGlobalSearch.Search));

            search.Clear();
            search.SendKeys(Constants.ValidateGlobalSearch.SearchTerm);

            WaitFor(By.XPath(Constants.ValidateGlobalSearch.Find));
            Click(By.XPath(Constants.ValidateGlobalSearch.Find));
        }

        private void PrepareSite()
        {
            driver.Navigate().GoToUrl(Constants.Url);
            driver.Manage().Window.Maximize();
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

        private void FillPositionForm()
        {
            WaitFor(By.Id(Constants.SearchForPositionBasedOnCriteria.KeywordsId));
            IWebElement keywords = driver.FindElement(By.Id(Constants.SearchForPositionBasedOnCriteria.KeywordsId));

            keywords.Clear();
            keywords.SendKeys(Constants.SearchForPositionBasedOnCriteria.ProgrammingLanguage);

            Click(By.XPath(Constants.SearchForPositionBasedOnCriteria.RemoteOption));
            Click(By.XPath(Constants.SearchForPositionBasedOnCriteria.Find));
        }

        private void ApplyToLatestElement()
        {
            WaitFor(By.XPath(Constants.SearchForPositionBasedOnCriteria.LatestElement));
            Click(By.XPath(Constants.SearchForPositionBasedOnCriteria.LatestElement));
            Click(By.XPath(Constants.SearchForPositionBasedOnCriteria.Apply));
        }
    }
}