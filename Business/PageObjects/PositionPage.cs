using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class PositionPage : BasePage
    {
        public PositionPage(BasePage basePage) : base(basePage)
        {
        }

        public bool Contains(string text)
        {
            try
            {
                return driver.PageSource.Contains(text);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while checking if text '{Text}' is present on the page.", text);
                throw new NoSuchElementException($"Text '{text}' not found on the page.", ex);
            }
        }
    }
}
