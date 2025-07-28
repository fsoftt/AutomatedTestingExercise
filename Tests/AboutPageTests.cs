using Business.PageObjects;
using Core.Utilities;
using CrossCutting.Providers;
using CrossCutting.Static;
using OpenQA.Selenium;

namespace Tests
{
    internal class AboutPageTests
    {
        private HomePage homePage;
        private ISimpleServiceProvider serviceProvider = new SimpleServiceProvider();

        [SetUp]
        public void Setup()
        {
            bool runAsHeadless = false;

            IWebDriver driver = serviceProvider.GetWebDriver(runAsHeadless);
            homePage = new HomePage(driver);
        }

        [TearDown]
        public void Teardown()
        {
            BrowserFactory.CloseDriver();
        }

        /*
         * Test case #3. Validate file download function works as expected:
            Create a Chrome instance.
            Navigate to https://www.epam.com/.
            Select “About” from the top menu.
            Scroll down to the “EPAM at a Glance” section.
            Click on the “Download” button.
            Wait till the file is downloaded.
            Validate that file “EPAM_Systems_Company_Overview.pdf” downloaded (use the name of the file as a parameter)
            Close the browser.
        */
        [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public void DownloadBrochure(string fileName)
        {
            AboutPage aboutPage = homePage.OpenAbout();

            bool downloaded = aboutPage.DownloadBrochure(fileName);
            if (!downloaded)
            {
                Assert.Fail("File was not downloaded.");
            }
        }
    }
}
