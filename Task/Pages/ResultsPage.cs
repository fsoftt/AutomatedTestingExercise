using OpenQA.Selenium;
using System.Collections.ObjectModel;
using EpamTask.Entities;

namespace EpamTask.Pages
{
    internal class ResultsPage : BasePage
    {
        private readonly By articlesBy = By.XPath(Constants.ValidateGlobalSearch.Articles);

        public ResultsPage(IWebDriver driver) : base(driver)
        {
        }

        public IEnumerable<Article> GetArticles()
        {
            WaitForElementToBeVisible(articlesBy);
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
