using OpenQA.Selenium;
using CrossCutting.Static;
using CrossCutting.Exceptions;

namespace Business.PageObjects
{
    public class HomePage : BasePage
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

            PrepareSite();
        }

        public IWebDriver PrepareSite()
        {
            driver.Navigate().GoToUrl(Constants.Url);
            AcceptCookies();

            return driver;
        }

        public HomePage AcceptCookies()
        {
            try
            {
                WaitForElementToBeVisible(cookiesBy);
                driver.FindElement(cookiesBy).Click();
            }
            catch (Exception)
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

            WaitForElementToBeVisible(searchInputBy);
            IWebElement search = driver.FindElement(searchInputBy);

            search.Clear();
            search.SendKeys(searchTerm);

            WaitForElementToBeVisible(findButtonBy);
            driver.FindElement(findButtonBy).Click();

            return new ResultsPage(driver);
        }
    }
}
