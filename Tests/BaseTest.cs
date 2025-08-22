using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Tests
{
    internal class BaseTest
    {
        protected IWebDriver driver;
        protected HomePage homePage;
        protected readonly ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

        [SetUp]
        public void Setup()
        {
            driver = serviceProvider.GetWebDriver();

            string url = GetUrl();
            driver.Navigate().GoToUrl(url);

            homePage = new HomePage(driver, serviceProvider);
        }

        private string GetUrl()
        {
            IConfiguration configuration = serviceProvider.GetConfiguration();
            string url = configuration.GetValue<string?>(ConfigurationKeys.Url)!;
            return url;
        }

        [TearDown]
        public void Teardown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotProvider.TakeBrowserScreenshot(homePage.driver);
            }

            driver.Dispose();
            BrowserFactory.CloseDriver(driver);
        }
    }
}
