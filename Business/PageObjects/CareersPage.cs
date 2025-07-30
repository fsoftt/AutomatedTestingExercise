using Business.PageObjects;
using CrossCutting.Exceptions;
using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class CareersPage : BasePage
    {
        private const string PageTitle = "Explore Professional Growth Opportunities | EPAM Careers";
        
        private readonly By applyBy;
        private readonly By locationBy;
        private readonly By findButtonBy;
        private readonly By allLocationsBy;
        private readonly By latestElementBy;
        private readonly By keywordsInputBy;
        private readonly By remoteCheckboxBy;

        public CareersPage(HomePage homePage) : base(homePage)
        {
            string title = driver.Title;
            if (title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }

            string applyById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.Apply)!;
            string locationById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.Location)!;
            string findButtonById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.Find)!;
            string allLocationsById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.AllLocations)!;
            string keywordsInputById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.KeywordsId)!;
            string latestElementById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.LatestElement)!;
            string remoteCheckboxById = configuration.GetValue<string?>(ConfigurationKeys.SearchForPositionBasedOnCriteria.RemoteOption)!;

            applyBy = By.XPath(applyById);
            locationBy = By.CssSelector(locationById);
            findButtonBy = By.XPath(findButtonById);
            allLocationsBy = By.CssSelector(allLocationsById);
            keywordsInputBy = By.Id(keywordsInputById);
            latestElementBy = By.XPath(latestElementById);
            remoteCheckboxBy = By.XPath(remoteCheckboxById);
        }

        public void Search(string keywords, bool isRemote)
        {
            logger.LogDebug("Searching for positions with keywords: {Keywords} and remote option: {IsRemote}", keywords, isRemote);

            SetKeywords(keywords);
            SetIsRemote(isRemote);
            
            Click(driver.FindElement(locationBy));

            WaitForElementToBeVisible(allLocationsBy);
            Click(driver.FindElement(allLocationsBy));

            Click(driver.FindElement(findButtonBy));
        }

        private void SetIsRemote(bool isRemote)
        {
            if (isRemote)
            {
                Click(driver.FindElement(remoteCheckboxBy));
            }
        }

        private void SetKeywords(string keywords)
        {
            WaitForElementToBeVisible(keywordsInputBy);
            IWebElement keywordsElement = driver.FindElement(keywordsInputBy);

            SendKeys(keywordsElement, keywords);
        }

        public PositionPage ApplyToLatestElement()
        {
            logger.LogDebug("Applying to the latest position element");

            WaitForElementToBeVisible(latestElementBy);
            Click(driver.FindElement(latestElementBy));
            Click(driver.FindElement(applyBy));

            return new PositionPage(this);
        }
    }
}
