using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using Task.Entities;
using Task.Exceptions;

namespace Task.Pages
{
    internal class AboutPage : BasePage
    {
        private const string PageTitle = "One of the Fastest-Growing Public Tech Companies | About EPAM";

        private readonly By articlesBy = By.XPath(Constants.ValidateGlobalSearch.Articles);

        public AboutPage(IWebDriver driver) : base(driver)
        {
            if (driver.Title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }
        }

        internal bool DownloadBrochure()
        {
            string filePath = Path.Combine(Constants.DownloadDirectory, Constants.BrochureFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            ReadOnlyCollection<IWebElement> linksWithDownload = driver.FindElements(By.XPath("//a[@download]"));
            if (linksWithDownload.Count == 0)
            {
                throw new NoSuchElementException("No link with 'download' attribute found.");
            }

            // TODO refactor to ScrollTo in base
            IWebElement downloadButton = linksWithDownload[0];
            ScrollTo(downloadButton);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => IsInViewport(downloadButton));

            downloadButton.Click();

            bool downloaded = wait.Until(driver =>
            {
                return File.Exists(filePath);
            });

            if (!downloaded)
            {
                throw new TimeoutException($"File '{filePath}' was not downloaded within {Constants.WaitTimeInSeconds} seconds.");
            }

            return downloaded;
        }
    }
}
