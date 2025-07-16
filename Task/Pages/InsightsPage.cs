using EpamTask.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTask.Pages
{
    internal class InsightsPage : BasePage
    {
        private readonly By carouselNextBy = By.CssSelector(Constants.Insights.CarouselNext);
        private readonly By carouselTitleBy = By.CssSelector(Constants.Insights.CurrentCarouselTitle);
        private readonly By readMoreButtonBy = By.CssSelector(Constants.Insights.ReadMoreButton);

        public InsightsPage(IWebDriver driver) : base(driver)
        {
        }

        internal string GetCarouselArticleTitle()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => driver.FindElement(carouselTitleBy).Displayed);

            return driver.FindElement(carouselTitleBy).Text;
        }

        internal InsightArticlePage OpenCarouselArticle()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => driver.FindElement(readMoreButtonBy).Displayed);

            driver.FindElement(readMoreButtonBy).Click();
            
            return new InsightArticlePage(driver);
        }

        internal void SwipeCarousel()
        {
            IWebElement next = driver.FindElement(carouselNextBy);
            next.Click();
        }
    }
}
