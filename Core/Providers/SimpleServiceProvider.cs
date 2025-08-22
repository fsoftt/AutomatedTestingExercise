using Core.Utilities;
using CrossCutting.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrossCutting.Providers
{
    public class SimpleServiceProvider : ISimpleServiceProvider
    {
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
    }
}
