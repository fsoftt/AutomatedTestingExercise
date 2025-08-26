using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using CrossCutting.Static;
using CrossCutting.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
            var logger = LoggingFactory.CreateLogger<BaseTest>();

            logger.LogDebug("Setting up the test.");
            IConfiguration testData = serviceProvider.GetTestData();
            logger.LogDebug("Retrieved test data.");
            IConfiguration configuration = serviceProvider.GetConfiguration();
            logger.LogDebug("Retrieved configuration.");

            string url = testData.GetValue<string?>(ConfigurationKeys.Url)!;
            string browser = Environment.GetEnvironmentVariable("TEST_BROWSER")?.ToLower() ?? configuration.GetValue<string?>(ConfigurationKeys.Browser)!;
            logger.LogDebug("Browser to use: " + browser);

            logger.LogDebug("Creating driver");
            driver = serviceProvider.GetWebDriver(BrowserTypeFactory.FromString(browser));
            logger.LogDebug("Driver created");

            logger.LogDebug("Navigating");
            driver.Navigate().GoToUrl(url);
            logger.LogDebug("Navigated");

            logger.LogDebug("Taking screenshot");
            ScreenshotProvider.TakeBrowserScreenshot(driver);
            logger.LogDebug("Screenshot taken");

            homePage = new HomePage(driver, serviceProvider);
        }

        [TearDown]
        public void Teardown()
        {
            ScreenshotProvider.TakeBrowserScreenshot(homePage.driver);
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotProvider.TakeBrowserScreenshot(homePage.driver);
            }

            driver.Dispose();
            BrowserFactory.CloseDriver(driver);
        }
    }
}
