using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrossCutting.Providers
{
    public interface ISimpleServiceProvider
    {
        IConfiguration GetConfiguration();
        ILogger GetLogger<T>();
        IWebDriver GetWebDriver(bool headless);
    }
}
