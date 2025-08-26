using CrossCutting.Providers;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Core.Utilities
{
    public static class ScreenshotProvider
    {
        public static string TakeBrowserScreenshot(IWebDriver driver)
        {
            LoggingFactory.CreateLogger<SimpleServiceProvider>().LogInformation("Taking screenshot of the browser window. Path: " + Environment.CurrentDirectory);

            var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
            var screenshotPath = $"D:/a/AutomatedTestingExercise/AutomatedTestingExercise/Tests/bin/Debug/net8.0/screenshots/Display_{now}.png";
            ((ITakesScreenshot) driver).GetScreenshot().SaveAsFile(screenshotPath);

            return screenshotPath;
        }
    }
}
