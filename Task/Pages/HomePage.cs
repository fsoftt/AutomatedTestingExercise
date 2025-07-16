using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EpamTask.Exceptions;

namespace EpamTask.Pages
{
    internal class HomePage : BasePage
    {
        private const string PageTitle = "EPAM | Software Engineering & Product Development Services";

        private readonly By cookiesBy = By.CssSelector(Constants.CookiesElementBy);
        private readonly By findButtonBy = By.XPath(Constants.ValidateGlobalSearch.Find);
        private readonly By searchInputBy = By.Name(Constants.ValidateGlobalSearch.Search);
        private readonly By magnifierIconBy = By.ClassName(Constants.ValidateGlobalSearch.Magnifier);
        private readonly By aboutLinkBy = By.LinkText(Constants.SearchForPositionBasedOnCriteria.AboutLinkText);
        private readonly By careersLinkBy = By.LinkText(Constants.SearchForPositionBasedOnCriteria.CareersLinkText);
        private readonly By insightsLinkBy = By.LinkText(Constants.SearchForPositionBasedOnCriteria.InsightsLinkText);
        
        public HomePage(IWebDriver driver) : base(driver)
        {
            string title = driver.Title;
            if (title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }
        }

        public async Task<HomePage> AcceptCookies()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
                wait.Until(driver => driver.FindElement(cookiesBy).Displayed);

                await Task.Delay(500);
                driver.FindElement(cookiesBy).Click();
            }
            catch (NoSuchElementException)
            {
            }
            
            return this;
        }

        public AboutPage OpenAbout()
        {
            driver.FindElement(aboutLinkBy).Click();

            return new AboutPage(driver);
        }

        public CareersPage OpenCareers()
        {
            driver.FindElement(careersLinkBy).Click();

            return new CareersPage(driver);
        }

        public InsightsPage OpenInsights()
        {
            driver.FindElement(insightsLinkBy).Click();

            return new InsightsPage(driver);
        }

        public ResultsPage Search(string searchTerm)
        {
            driver.FindElement(magnifierIconBy).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => driver.FindElement(searchInputBy).Displayed);

            IWebElement search = driver.FindElement(searchInputBy);

            search.Clear();
            search.SendKeys(searchTerm);

            wait.Until(driver => driver.FindElement(findButtonBy).Displayed);
            driver.FindElement(findButtonBy).Click();

            return new ResultsPage(driver);
        }
    }
}
