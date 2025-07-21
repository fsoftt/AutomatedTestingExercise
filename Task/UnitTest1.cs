using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EpamTask.Entities;
using EpamTask.Pages;

namespace EpamTask
{
    public class UnitTest1
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", Constants.DownloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);

            bool runAsHeadless = false;
            if (runAsHeadless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }

            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        /*
         * Test case #1. Validate that the user can search for a position based on criteria.
            Navigate to https://www.epam.com/
            Find a link “Carriers” and click on it
            Write the name of any programming language in the field “Keywords” (should be taken from test parameter)
            Select “All Locations” in the “Location” field (should be taken from the test parameter)
            Select the option “Remote”
            Click on the button “Find”
            Find the latest element in the list of results
            Click on the button “View and apply”
            Validate that the programming language mentioned in the step above is on a page
        */
        [TestCase("C#")]
        [TestCase("Java")]
        [TestCase("Python")]
        public void SearchForPositionBasedOnCriteria(string programmingLanguage)
        {
            driver = PrepareSite();

            HomePage homePage = GetHomePage(driver);
            CareersPage careersPage = homePage.OpenCareers();
            careersPage.Search(programmingLanguage, isRemote: true);

            PositionPage positionPage = careersPage.ApplyToLatestElement();

            bool containsKeyword = positionPage.Contains(programmingLanguage);

            Assert.That(containsKeyword, Is.True, "Page does not contain the used keyword.");
        }

        /*
            Test case #2. Validate global search works as expected.
            Navigate to https://www.epam.com/
            Find a magnifier icon and click on it
            Find a search string and put there “BLOCKCHAIN”/”Cloud”/”Automation” (use as a parameter for a test)
            Click “Find” button
            Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used. 
        */
        [TestCase("Cloud")]
        [TestCase("Blockchain")]
        [TestCase("Automation")]
        public void ValidateGlobalSearch(string searchTerm)
        {
            driver = PrepareSite();

            HomePage homePage = GetHomePage(driver);
            ResultsPage resultsPage = homePage.Search(searchTerm);

            IEnumerable<Article> articles = resultsPage.GetArticles();

            foreach (Article article in articles)
            {
                Assert.That(article.Title.ToLower().Contains(searchTerm.ToLower())
                    || article.Description.ToLower().Contains(searchTerm.ToLower()), Is.True,
                    $"Article '{article.Title}' does not contain the search term '{searchTerm}'.");
            }
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
            driver = PrepareSite();

            HomePage homePage = GetHomePage(driver);
            AboutPage aboutPage = homePage.OpenAbout();

            bool downloaded = aboutPage.DownloadBrochure(fileName);
            if (!downloaded)
            {
                Assert.Fail("File was not downloaded.");
            }
        }

        /*
        Test case #4. Validate title of the article matches with title in the carousel:
            Create a Chrome instance.
            Navigate to https://www.epam.com/.
            Select “Insights” from the top menu.
            Swipe a carousel twice.
            Note the name of the article.
            Click on the “Read More” button.
            Validate that the name of the article matches with the noted above. 
            Close the browser.
        */
        [Test]
        public void CheckInsights()
        {
            driver = PrepareSite();

            HomePage homePage = GetHomePage(driver);
            InsightsPage insightsPage = homePage.OpenInsights();

            insightsPage.SwipeCarousel();
            insightsPage.SwipeCarousel();

            string articleTitle = insightsPage.GetCarouselArticleTitle();
            InsightArticlePage articlePage = insightsPage.OpenCarouselArticle();

            string articlePageTitle = articlePage.GetTitle();

            Assert.That(articlePageTitle, Contains.Substring(articleTitle),
                $"Article title '{articlePageTitle}' does not match the expected title '{articleTitle}'.");
        }

        private IWebDriver PrepareSite()
        {
            driver.Navigate().GoToUrl(Constants.Url);
            driver.Manage().Window.Maximize();

            return driver;
        }

        private HomePage GetHomePage(IWebDriver driver)
        {
            var homePage = new HomePage(driver);
            homePage.AcceptCookies();

            return homePage;
        }
    }
}