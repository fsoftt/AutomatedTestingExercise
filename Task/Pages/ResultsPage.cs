using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using Task.Entities;
using Task.Exceptions;

namespace Task.Pages
{
    internal class ResultsPage : BasePage
    {
        private const string PageTitle = "Search Our Website | EPAM";

        private readonly By articlesBy = By.XPath(Constants.ValidateGlobalSearch.Articles);

        public ResultsPage(IWebDriver driver) : base(driver)
        {
            if (driver.Title != PageTitle)
            {
                throw new IllegalStateException("Page is different than expected", driver.Url);
            }
        }

        public IEnumerable<Article> GetArticles()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WaitTimeInSeconds));
            wait.Until(driver => driver.FindElement(articlesBy).Displayed);

            ReadOnlyCollection<IWebElement> articlesElement = driver.FindElements(articlesBy);
            
            var articles = new List<Article>();
            foreach (IWebElement element in articlesElement)
            {
                string title = element.FindElement(By.TagName("h3")).Text;
                string description = element.FindElement(By.TagName("p")).Text;

                articles.Add(new Article
                {
                    Title = title,
                    Description = description
                });
            }

            return articles;
        }
    }
}
