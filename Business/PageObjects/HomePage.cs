using CrossCutting.Exceptions;
using CrossCutting.Providers;
using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class HomePage : BasePage
    {
        private const string PageTitle = "EPAM | Software Engineering & Product Development Services";

        private readonly By cookiesBy;
        private readonly By findButtonBy;
        private readonly By searchInputBy;
        private readonly By magnifierIconBy;
        private readonly By aboutLinkBy;
        private readonly By careersLinkBy;
        private readonly By servicesLinkBy;
        private readonly By insightsLinkBy;

        public HomePage(IWebDriver driver, ISimpleServiceProvider serviceProvider) : base(driver, serviceProvider)
        {
            this.driver = driver;
            string cookiesById = testData.GetValue<string?>(ConfigurationKeys.CookiesElementBy)!;
            string findButtonById = testData.GetValue<string?>(ConfigurationKeys.ValidateGlobalSearch.Find)!;
            string servicesLinkById = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.LinkText)!;
            string searchInputById = testData.GetValue<string?>(ConfigurationKeys.ValidateGlobalSearch.Search)!;
            string magnifierIconById = testData.GetValue<string?>(ConfigurationKeys.ValidateGlobalSearch.Magnifier)!;
            string aboutLinkById = testData.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.AboutLinkText)!;
            string careersLinkById = testData.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.CareersLinkText)!;
            string insightsLinkById = testData.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.InsightsLinkText)!;

            cookiesBy = By.CssSelector(cookiesById);
            findButtonBy = By.XPath(findButtonById);
            searchInputBy = By.Name(searchInputById);
            magnifierIconBy = By.ClassName(magnifierIconById);
            aboutLinkBy = By.LinkText(aboutLinkById);
            careersLinkBy = By.LinkText(careersLinkById);
            insightsLinkBy = By.LinkText(insightsLinkById);
            servicesLinkBy = By.LinkText(servicesLinkById);

            PrepareSite();

            string title = driver.Title;
            if (title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }
        }

        public IWebDriver PrepareSite()
        {
            AcceptCookies();

            return driver;
        }

        public HomePage AcceptCookies()
        {
            logger.LogInformation("Accepting cookies");

            try
            {
                WaitForElementToBeVisible(cookiesBy);
                Click(driver.FindElement(cookiesBy));
            }
            catch (Exception)
            {
                logger.LogWarning("Cookies element not found or already accepted");
            }
            
            return this;
        }

        public AboutPage OpenAbout()
        {
            logger.LogInformation("Opening About page");

            Click(driver.FindElement(aboutLinkBy));

            return new AboutPage(this);
        }

        public CareersPage OpenCareers()
        {
            logger.LogInformation("Opening Careers page");

            Click(driver.FindElement(careersLinkBy));

            return new CareersPage(this);
        }

        public InsightsPage OpenInsights()
        {
            logger.LogInformation("Opening Insights page");

            Click(driver.FindElement(insightsLinkBy));

            return new InsightsPage(this);
        }

        public ServicesPage OpenServices()
        {
            logger.LogInformation("Opening Services page");

            Click(driver.FindElement(servicesLinkBy));

            return new ServicesPage(this);
        }

        public ResultsPage Search(string searchTerm)
        {
            logger.LogInformation("Searching for term: {SearchTerm}", searchTerm);

            Click(driver.FindElement(magnifierIconBy));

            WaitForElementToBeVisible(searchInputBy);
            IWebElement search = driver.FindElement(searchInputBy);

            SendKeys(search, searchTerm);

            WaitForElementToBeVisible(findButtonBy);
            Click(driver.FindElement(findButtonBy));

            return new ResultsPage(this);
        }
    }
}
