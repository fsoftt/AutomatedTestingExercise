using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class InsightsPage : BasePage
    {
        private readonly By carouselNextBy;
        private readonly By carouselTitleBy;
        private readonly By readMoreButtonBy;

        public InsightsPage(BasePage basePage) : base(basePage)
        {
            string carouselNextById = configuration.GetValue<string>(ConfigurationKeys.Insights.CarouselNext)!;
            string carouselTitleById = configuration.GetValue<string>(ConfigurationKeys.Insights.CurrentCarouselTitle)!;
            string readMoreButtonById = configuration.GetValue<string>(ConfigurationKeys.Insights.ReadMoreButton)!;

            carouselNextBy = By.CssSelector(carouselNextById);
            carouselTitleBy = By.CssSelector(carouselTitleById);
            readMoreButtonBy = By.CssSelector(readMoreButtonById);
        }

        public string GetCarouselArticleTitle()
        {
            logger.LogDebug("Getting carousel article title");

            WaitForElementToBeVisible(carouselTitleBy);
            return driver.FindElement(carouselTitleBy).Text;
        }

        public InsightArticlePage OpenCarouselArticle()
        {
            logger.LogDebug("Opening carousel article");

            WaitForElementToBeVisible(readMoreButtonBy);
            Click(driver.FindElement(readMoreButtonBy));
            
            return new InsightArticlePage(this);
        }

        public void SwipeCarousel()
        {
            logger.LogDebug("Swiping carousel");

            IWebElement next = driver.FindElement(carouselNextBy);
            Click(next);
        }
    }
}
