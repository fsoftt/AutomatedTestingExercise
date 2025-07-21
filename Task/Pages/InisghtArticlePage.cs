using OpenQA.Selenium;

namespace EpamTask.Pages
{
    internal class InsightArticlePage : BasePage
    {
        private readonly By titleBy = By.CssSelector("div.container div.header_and_download");

        public InsightArticlePage(IWebDriver driver) : base(driver)
        {
        }

        internal string GetTitle()
        {
            WaitForElementToBeVisible(titleBy);
            return driver.FindElement(titleBy).Text;
        }
    }
}
