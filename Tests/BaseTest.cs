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

            IConfiguration testData = serviceProvider.GetTestData();
            IConfiguration configuration = serviceProvider.GetConfiguration();

            string url = testData.GetValue<string?>(ConfigurationKeys.Url)!;
            string browser = Environment.GetEnvironmentVariable("TEST_BROWSER")?.ToLower() ?? configuration.GetValue<string?>(ConfigurationKeys.Browser)!;
            logger.LogDebug("Browser to use: " + browser);

            logger.LogDebug("Creating driver");
            driver = serviceProvider.GetWebDriver(BrowserTypeFactory.FromString(browser));
            logger.LogDebug("Driver created");

            driver.Navigate().GoToUrl(url);

            homePage = new HomePage(driver, serviceProvider);
        }

        [TearDown]
        public void Teardown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                ScreenshotProvider.TakeBrowserScreenshot(driver);
            }

            driver.Dispose();
            BrowserFactory.CloseDriver(driver);
        }
    }
}
