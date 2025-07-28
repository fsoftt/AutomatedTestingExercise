using Core.Utilities;
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

        public IWebDriver GetWebDriver(bool headless)
        {
            return BrowserFactory.GetDriver("chrome", headless);
        }
    }
}
