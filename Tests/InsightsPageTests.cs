using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using NUnit.Framework.Interfaces;

namespace Tests
{
    internal class InsightsPageTests
    {
        private HomePage homePage;
        private readonly ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

        [SetUp]
        public void Setup()
        {
            homePage = new HomePage(serviceProvider);
        }

        [TearDown]
        public void Teardown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotProvider.TakeBrowserScreenshot(homePage.driver);
            }

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
