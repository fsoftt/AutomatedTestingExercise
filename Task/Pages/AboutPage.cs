using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using EpamTask.Entities;
using EpamTask.Exceptions;

namespace EpamTask.Pages
{
    internal class AboutPage : BasePage
    {
        private const string PageTitle = "One of the Fastest-Growing Public Tech Companies | About EPAM";

        private readonly By downloadBy = By.XPath(Constants.Download.DownloadButton);

        public AboutPage(IWebDriver driver, bool headless) : base(driver, headless)
        {
            string title = driver.Title;
            if (!headless && title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }
        }

        internal bool DownloadBrochure(string fileName)
        {
            string filePath = Path.Combine(Constants.DownloadDirectory, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            ReadOnlyCollection<IWebElement> linksWithDownload = driver.FindElements(downloadBy);
            if (linksWithDownload.Count == 0)
            {
                throw new NoSuchElementException("No link with 'download' attribute found.");
            }

            IWebElement downloadButton = linksWithDownload[0];
            ScrollTo(downloadButton);

            WaitForElementToBeInViewport(downloadButton);
            downloadButton.Click();

            bool downloaded = WaitFor(driver =>
            {
                return File.Exists(filePath);
            });

            return downloaded;
        }
    }
}
