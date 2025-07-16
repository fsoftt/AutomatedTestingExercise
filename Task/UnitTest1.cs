using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task.Entities;
using Task.Pages;

namespace Task
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
        [Test]
        public void SearchForPositionBasedOnCriteria()
        {
            try
            {
                driver = PrepareSite();

                CareersPage careersPage = new HomePage(driver).OpenCareers();
                careersPage.Search(Constants.SearchForPositionBasedOnCriteria.ProgrammingLanguage, isRemote: true);

                PositionPage positionPage = careersPage.ApplyToLatestElement();

                Assert.That(positionPage, Is.Not.Null, "Position page should not be null.");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /*
            Test case #2. Validate global search works as expected.
            Navigate to https://www.epam.com/
            Find a magnifier icon and click on it
            Find a search string and put there “BLOCKCHAIN”/”Cloud”/”Automation” (use as a parameter for a test)
            Click “Find” button
            Validate that all links in a list contain the word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used. 
        */

        [Test]
        public void ValidateGlobalSearch()
        {
            try
            {
                driver = PrepareSite();

                HomePage homePage = new HomePage(driver);
                ResultsPage resultsPage = homePage.Search(Constants.ValidateGlobalSearch.SearchTerm);

                IEnumerable<Article> articles = resultsPage.GetArticles();

                foreach (Article article in articles)
                {
                    Assert.That(article.Title.ToLower().Contains(Constants.ValidateGlobalSearch.SearchTerm.ToLower()) 
                        || article.Description.ToLower().Contains(Constants.ValidateGlobalSearch.SearchTerm.ToLower()), Is.True,
                        $"Article '{article.Title}' does not contain the search term '{Constants.ValidateGlobalSearch.SearchTerm}'.");
                }
            }
            catch (Exception)
            {
                Assert.Fail();
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
        [Test]
        public void DownloadBrochure()
        {
            try
            {
                driver = PrepareSite();

                HomePage homePage = new HomePage(driver);
                AboutPage aboutPage = homePage.OpenAbout();

                bool downloaded= aboutPage.DownloadBrochure();
                if (!downloaded)
                {
                    Assert.Fail("File was not downloaded.");
                }
            }
            catch (Exception)
            {
                Assert.Fail();
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


        private IWebDriver PrepareSite()
        {
            driver.Navigate().GoToUrl(Constants.Url);
            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}