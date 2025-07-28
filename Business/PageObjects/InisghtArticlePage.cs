using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class InsightArticlePage : BasePage
    {
        private readonly By titleBy = By.CssSelector("div.container div.header_and_download");

        public InsightArticlePage(IWebDriver driver) : base(driver)
        {
        }

        public string GetTitle()
        {
            WaitForElementToBeVisible(titleBy);
            return driver.FindElement(titleBy).Text;
        }
    }
}
