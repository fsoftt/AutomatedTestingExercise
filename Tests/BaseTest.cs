using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using CrossCutting.Static;
using CrossCutting.Types;
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
            IConfiguration testData = serviceProvider.GetTestData();
            IConfiguration configuration = serviceProvider.GetConfiguration();
            
            string url = testData.GetValue<string?>(ConfigurationKeys.Url)!;
            string browser = configuration.GetValue<string?>(ConfigurationKeys.Browser)!;

            driver = serviceProvider.GetWebDriver(BrowserTypeFactory.FromString(browser));

            driver.Navigate().GoToUrl(url);

            homePage = new HomePage(driver, serviceProvider);
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
