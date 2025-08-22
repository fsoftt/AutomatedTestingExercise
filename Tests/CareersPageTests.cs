using Business.PageObjects;

namespace Tests
{
    internal class CareersPageTests : BaseTest
    {
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
            CareersPage careersPage = homePage.OpenCareers();
            careersPage.Search(programmingLanguage, isRemote: true);

            PositionPage positionPage = careersPage.ApplyToLatestElement();

            bool containsKeyword = positionPage.Contains(programmingLanguage);

            Assert.That(containsKeyword, Is.True, "Page does not contain the used keyword.");
        }
    }
}
