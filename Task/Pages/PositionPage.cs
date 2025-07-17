using OpenQA.Selenium;

namespace EpamTask.Pages
{
    internal class PositionPage : BasePage
    {
        public PositionPage(IWebDriver driver, bool headless) : base(driver, headless)
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
