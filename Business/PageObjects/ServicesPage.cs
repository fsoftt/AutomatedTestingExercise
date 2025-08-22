using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class ServicesPage : BasePage
    {
        private readonly By menuBy;
        private readonly By titleBy;
        private readonly By servicesMenuBy;
        private readonly By ourRelatedExpertiseBy;
        private readonly string objectiveMenuSelector;
        private readonly By artificialIntelligenceMenuBy;

        public ServicesPage(HomePage homePage) : base(homePage)
        {
            string menuBySelector = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.MenuBy)!;
            menuBy = By.CssSelector(menuBySelector);

            string titleBySelector = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.TitleBy)!;
            titleBy = By.CssSelector(titleBySelector);

            string servicesMenuBySelector = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.ServicesMenuBy)!;
            servicesMenuBy = By.XPath(servicesMenuBySelector);

            string ourRelatedExpertiseSelector = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.OurRelatedExpertiseBy)!;
            ourRelatedExpertiseBy = By.XPath(ourRelatedExpertiseSelector);

            string artificialIntelligenceMenuBySelector = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.ArtificialIntelligenceMenuBy)!;
            artificialIntelligenceMenuBy = By.XPath(artificialIntelligenceMenuBySelector);

            objectiveMenuSelector = testData.GetValue<string?>(ConfigurationKeys.ValidateServices.ObjectiveMenuBy)!;
        }

        public ServicesPage OpenAiMenu(string menuOption)
        {
            logger.LogDebug("Opening side menu");
            WaitFor(x => x.FindElement(menuBy).Displayed);
            IWebElement menu = driver.FindElement(menuBy);
            menu.Click();

            logger.LogDebug("Opening services menu");
            WaitFor(x => x.FindElement(servicesMenuBy).Displayed);
            IWebElement servicesMenu = driver.FindElement(servicesMenuBy);
            servicesMenu.Click();

            logger.LogDebug("Opening AI menu");
            WaitFor(x => servicesMenu.FindElement(artificialIntelligenceMenuBy).Displayed);
            IWebElement artificialIntelligenceMenu = servicesMenu.FindElement(artificialIntelligenceMenuBy);
            artificialIntelligenceMenu.Click();

            logger.LogDebug("Opening final menu");

            string selector = string.Format(objectiveMenuSelector, menuOption);
            By finalBy = By.XPath(selector);

            WaitFor(x => artificialIntelligenceMenu.FindElement(finalBy).Displayed);
            IWebElement objectiveMenu = artificialIntelligenceMenu.FindElement(finalBy);
            objectiveMenu.Click();

            return this;
        }

        public string GetTitle()
        {
            logger.LogDebug("Getting page title");
            return GetElementText(titleBy);

        }

        public string GetOurRelatedExpertise()
        {
            logger.LogDebug("Getting 'Our Related Expertise' section");
            return GetElementText(ourRelatedExpertiseBy);
        }

        public string GetElementText(By by)
        {
            WaitForElementToBeVisible(by);
            IWebElement element = driver.FindElement(by);

            return element.Text;
        }
    }
}
