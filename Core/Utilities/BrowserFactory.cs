using CrossCutting.Static;
using CrossCutting.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.Utilities
{
    public static class BrowserFactory
    {
        public static IWebDriver GetDriver(BrowserType browserType, bool headless = false)
        {
            IWebDriver? driver = null;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    // TODO Options can be created in a provider
                    var guid = Guid.NewGuid();
                    var options = new ChromeOptions();
                    options.AddUserProfilePreference("download.default_directory", "/tmp/chrome-user-data-{guid}");
                    options.AddUserProfilePreference("download.prompt_for_download", false);
                    options.AddUserProfilePreference("disable-popup-blocking", true);
                    options.AddArgument($"--user-data-dir=/tmp/chrome-user-data-{guid}");

                    if (headless)
                    {
                        options.AddArgument("--headless=new");
                        options.AddArgument("--disable-gpu");
                        options.AddArgument("--window-size=1920,1080");
                    }

                    driver = new ChromeDriver(options);
                    break;
                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();

                    if (headless)
                    {
                        firefoxOptions.AddArgument("--headless=new");
                        firefoxOptions.AddArgument("--disable-gpu");
                        firefoxOptions.AddArgument("--window-size=1920,1080");
                    }

                    driver = new FirefoxDriver(firefoxOptions);
                    break;
                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions();

                    if (headless)
                    {
                        edgeOptions.AddArgument("--headless=new");
                        edgeOptions.AddArgument("--disable-gpu");
                        edgeOptions.AddArgument("--window-size=1920,1080");
                    }
                    driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException($"Browser type '{browserType}' is not supported.");
            }

            driver
                .Manage().Window
                .Maximize();

            return driver!;
        }

        public static void CloseDriver(IWebDriver? driver)
        {
            if (driver == null)
            {
                return;
            }

            driver.Quit();
            driver = null;
        }
    }        
}
