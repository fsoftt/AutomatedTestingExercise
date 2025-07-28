using CrossCutting.Static;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class InsightsPage : BasePage
    {
        private readonly By carouselNextBy = By.CssSelector(Constants.Insights.CarouselNext);
        private readonly By carouselTitleBy = By.CssSelector(Constants.Insights.CurrentCarouselTitle);
        private readonly By readMoreButtonBy = By.CssSelector(Constants.Insights.ReadMoreButton);

        public InsightsPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetCarouselArticleTitle()
        {
            WaitForElementToBeVisible(carouselTitleBy);
            return driver.FindElement(carouselTitleBy).Text;
        }

        public InsightArticlePage OpenCarouselArticle()
        {
            WaitForElementToBeVisible(readMoreButtonBy);
            driver.FindElement(readMoreButtonBy).Click();
            
            return new InsightArticlePage(driver);
        }

        public void SwipeCarousel()
        {
            IWebElement next = driver.FindElement(carouselNextBy);
            next.Click();
        }
    }
}
