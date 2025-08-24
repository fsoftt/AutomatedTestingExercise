using Core.Utilities;
using CrossCutting.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using RestSharp;

namespace CrossCutting.Providers
{
    public class SimpleServiceProvider : ISimpleServiceProvider
    {
        public IConfiguration GetTestData()
        {
            return ConfigFactory.GetTestData();
        }

        public IConfiguration GetConfiguration()
        {
            return ConfigFactory.Get();
        }

        public ILogger GetLogger<T>()
        {
            return LoggingFactory.CreateLogger<T>();
        }

        public IWebDriver GetWebDriver(BrowserType browserType, bool headless = false)
        {
            return BrowserFactory.GetDriver(browserType, headless);
        }

        public RestSharpClient GetRestClient()
        {
            var logger = GetLogger<RestSharpClient>();

            return RestFactory.Create(logger);
        }
    }
}
