using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.Utilities
{
    public static class BrowserFactory
    {
        private static IWebDriver? driver;

        // TODO create enum
        public static IWebDriver GetDriver(string browserType, bool headless = false)
        {
            if (driver != null)
            {
                return driver;
            }

            switch (browserType.ToLower())
            {
                case "chrome":
                    // TODO Options can be created in a provider
                    var options = new ChromeOptions();

                    if (headless)
                    {
                        options.AddArgument("--headless=new");
                        options.AddArgument("--disable-gpu");
                        options.AddArgument("--window-size=1920,1080");
                    }

                    driver = new ChromeDriver(options);
                    break;
                case "firefox":
                    var firefoxOptions = new FirefoxOptions();

                    if (headless)
                    {
                        firefoxOptions.AddArgument("--headless=new");
                        firefoxOptions.AddArgument("--disable-gpu");
                        firefoxOptions.AddArgument("--window-size=1920,1080");
                    }

                    driver = new FirefoxDriver(firefoxOptions);
                    break;
                case "edge":
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

            return driver;
        }

        public static void CloseDriver()
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
