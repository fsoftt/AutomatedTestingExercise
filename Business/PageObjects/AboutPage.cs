using CrossCutting.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Business.PageObjects
{
    public class AboutPage : BasePage
    {
        private readonly By downloadBy;

        public AboutPage(HomePage homePage) : base(homePage)
        {
            string downloadById = testData.GetValue<string?>(ConfigurationKeys.Download.DownloadButton)!;
            downloadBy = By.XPath(downloadById);
        }

        public bool DownloadBrochure(string fileName)
        {
            logger.LogDebug("Downloading brochure with file name: {FileName}", fileName);

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

            WaitForElementToBeVisible(downloadButton);
            Click(downloadButton);

            bool downloaded = WaitFor(driver =>
            {
                return File.Exists(filePath);
            });

            return downloaded;
        }
    }
}
