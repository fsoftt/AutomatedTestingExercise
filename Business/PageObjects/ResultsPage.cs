using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Business.Entities;
using CrossCutting.Static;

namespace Business.PageObjects
{
    public class ResultsPage : BasePage
    {
        private readonly By titleBy = By.TagName("h3");
        private readonly By descriptionBy = By.TagName("p");
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
                string title = element.FindElement(titleBy).Text;
                string description = element.FindElement(descriptionBy).Text;

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
