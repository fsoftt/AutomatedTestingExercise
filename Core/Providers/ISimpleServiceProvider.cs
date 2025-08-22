using CrossCutting.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrossCutting.Providers
{
    public interface ISimpleServiceProvider
    {
        IConfiguration GetTestData();
        IConfiguration GetConfiguration();
        ILogger GetLogger<T>();
        IWebDriver GetWebDriver(BrowserType browserType, bool headless = false);
    }
}
