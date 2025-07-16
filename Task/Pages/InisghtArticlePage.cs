using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => driver.FindElement(titleBy).Displayed);

            return driver.FindElement(titleBy).Text;
        }
    }
}
