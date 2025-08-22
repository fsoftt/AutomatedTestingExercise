using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using NUnit.Framework.Interfaces;

namespace Tests
{
    internal class BaseTest
    {
        protected HomePage homePage;
        protected readonly ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

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
    }
}
