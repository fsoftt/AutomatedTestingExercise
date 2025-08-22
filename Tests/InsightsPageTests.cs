using Business.PageObjects;

namespace Tests
{
    internal class InsightsPageTests : BaseTest
    {
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
