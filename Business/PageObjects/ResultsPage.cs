using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Business.Entities;
using Microsoft.Extensions.Configuration;
using CrossCutting.Static;
using Microsoft.Extensions.Logging;

namespace Business.PageObjects
{
    public class ResultsPage : BasePage
    {
        private readonly By titleBy;
        private readonly By articlesBy;
        private readonly By descriptionBy;

        public ResultsPage(HomePage homePage) : base(homePage)
        {
            string? articleTitleId = testData.GetValue<string?>(ConfigurationKeys.ValidateGlobalSearch.ArticleTitle);
            titleBy = By.TagName(articleTitleId!);

            string? articleTitleDescriptionId = testData.GetValue<string?>(ConfigurationKeys.ValidateGlobalSearch.ArticleDescription);
            descriptionBy = By.TagName(articleTitleDescriptionId!);

            string? articlesById = testData.GetValue<string?>(ConfigurationKeys.ValidateGlobalSearch.Articles);
            articlesBy = By.XPath(articlesById!);
        }

        public IEnumerable<Article> GetArticles()
        {
            logger.LogDebug("Getting articles from results page");

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
