using CrossCutting.Static;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.Utilities
{
    public static class WebDriverFactory
    {
        private static IWebDriver? driver;

        public static IWebDriver Get(bool headless = false)
        {
            if (driver == null)
            {
                Initialize(headless);
            }

            return driver!;
        }

        private static void Initialize(bool headless)
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", Constants.DownloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            if (headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }

            driver = new ChromeDriver(options);
        }
    }
}
