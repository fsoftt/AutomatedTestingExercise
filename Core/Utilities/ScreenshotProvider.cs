using OpenQA.Selenium;

namespace Core.Utilities
{
    public static class ScreenshotProvider
    {
        public static string TakeBrowserScreenshot(IWebDriver driver)
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
            var screenshotPath = $"/screenshots/Display_{now}.png";
            ((ITakesScreenshot) driver).GetScreenshot().SaveAsFile(screenshotPath);

            return screenshotPath;
        }
    }
}
