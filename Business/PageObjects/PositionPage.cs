using OpenQA.Selenium;

namespace Business.PageObjects
{
    public class PositionPage : BasePage
    {
        public PositionPage(IWebDriver driver) : base(driver)
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
                throw new NoSuchElementException($"Text '{text}' not found on the page.", ex);
            }
        }
    }
}
