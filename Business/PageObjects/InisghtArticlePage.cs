using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class InsightArticlePage : BasePage
    {
        private readonly By titleBy;

        public InsightArticlePage(BasePage basePage) : base(basePage)
        {
            string titleById = configuration.GetValue<string?>(ConfigurationKeys.Insights.InsightArticleTitle)!;
            titleBy = By.CssSelector(titleById);
        }

        public string GetTitle()
        {
            logger.LogDebug("Getting article title");

            WaitForElementToBeVisible(titleBy);
            return driver.FindElement(titleBy).Text;
        }
    }
}
