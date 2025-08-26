using Business.PageObjects;

namespace Tests
{
    internal class ServicesPageTests : BaseTest
    {
        /*
         *  Test case #1. Validate Navigation to Services Section
            Navigate to https://www.epam.com/
            Locate and click on the "Services" link in the main navigation menu.
            From the dropdown, select a specific service category: “Generative AI” or “Responsible AI” (parameterize the category selection).
            Validate that the page contains the correct title.
            Validate that the section ‘Our Related Expertise’ is displayed on the page
        */
        [TestCase("Responsible AI")]
        [TestCase("Generative AI")]
        [Category("UI")]
        public void OpenServiceMenu(string menuOption)
        {
            ServicesPage servicesPage = homePage.OpenServices();
            servicesPage = servicesPage.OpenAiMenu(menuOption);

            string title = servicesPage.GetTitle();
            Assert.That(title, Is.EqualTo(menuOption));

            string expertise = servicesPage.GetOurRelatedExpertise();
            Assert.That(expertise, Is.Not.Null.Or.Empty, "Our Related Expertise section should not be empty");
        }
    }
}
