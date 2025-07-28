using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Tests
{
    internal class InsightsPageTests
    {
        private HomePage homePage;
        private ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

        [SetUp]
        public void Setup()
        {
            bool runAsHeadless = false;

            IWebDriver driver = serviceProvider.GetWebDriver(runAsHeadless);
            homePage = new HomePage(driver);
        }

        [TearDown]
        public void Teardown()
        {
            BrowserFactory.CloseDriver();
        }

        [Test]
        public void CheckInsights()
        {
            InsightsPage insightsPage = homePage.OpenInsights();

            insightsPage.SwipeCarousel();
            insightsPage.SwipeCarousel();

            string articleTitle = insightsPage.GetCarouselArticleTitle();
            InsightArticlePage articlePage = insightsPage.OpenCarouselArticle();

            string articlePageTitle = articlePage.GetTitle();

            Assert.That(articlePageTitle, Contains.Substring(articleTitle),
                $"Article title '{articlePageTitle}' does not match the expected title '{articleTitle}'.");
        }
    }
}
