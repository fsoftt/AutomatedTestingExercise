using OpenQA.Selenium;
using CrossCutting.Exceptions;
using CrossCutting.Static;

namespace Business.PageObjects
{
    public class CareersPage : BasePage
    {
        private const string PageTitle = "Explore Professional Growth Opportunities | EPAM Careers";
        
        private readonly By applyBy = By.XPath(Constants.SearchForPositionBasedOnCriteria.Apply);
        private readonly By findButtonBy = By.XPath(Constants.SearchForPositionBasedOnCriteria.Find);
        private readonly By keywordsInputBy = By.Id(Constants.SearchForPositionBasedOnCriteria.KeywordsId);
        private readonly By remoteCheckboxBy = By.XPath(Constants.SearchForPositionBasedOnCriteria.RemoteOption);
        private readonly By latestElementBy = By.XPath(Constants.SearchForPositionBasedOnCriteria.LatestElement);
        private readonly By allLocationsBy = By.CssSelector(Constants.SearchForPositionBasedOnCriteria.AllLocations);
        private readonly By locationBy = By.CssSelector(Constants.SearchForPositionBasedOnCriteria.Location);

        public CareersPage(IWebDriver driver) : base(driver)
        {
            string title = driver.Title;
            if (title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }
        }

        public void Search(string keywords, bool isRemote)
        {
            SetKeywords(keywords);
            SetIsRemote(isRemote);
            
            driver.FindElement(locationBy).Click();

            WaitForElementToBeVisible(allLocationsBy);
            driver.FindElement(allLocationsBy).Click();

            driver.FindElement(findButtonBy).Click();
        }

        private void SetIsRemote(bool isRemote)
        {
            if (isRemote)
            {
                driver.FindElement(remoteCheckboxBy).Click();
            }
        }

        private void SetKeywords(string keywords)
        {
            WaitForElementToBeVisible(keywordsInputBy);
            IWebElement keywordsElement = driver.FindElement(keywordsInputBy);

            keywordsElement.Clear();
            keywordsElement.SendKeys(keywords);
        }

        public PositionPage ApplyToLatestElement()
        {
            WaitForElementToBeVisible(latestElementBy);
            driver.FindElement(latestElementBy).Click();
            driver.FindElement(applyBy).Click();

            return new PositionPage(driver);
        }
    }
}
