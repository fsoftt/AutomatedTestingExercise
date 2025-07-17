using OpenQA.Selenium;

namespace EpamTask.Pages
{
    public abstract class BasePage
    {
        protected bool headless;
        protected IWebDriver driver;

        public BasePage(IWebDriver driver, bool headless)
        {
            this.headless = headless;
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        protected void ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        protected bool IsInViewport(IWebElement element)
        {
            bool? isInViewport = (bool?)((IJavaScriptExecutor)driver).ExecuteScript(@"
                function isInViewport(element)
                {
                    const rect = element.getBoundingClientRect();
                    return (
                        rect.top >= 0 &&
                        rect.left >= 0 &&
                        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                        rect.right <= (window.innerWidth || document.documentElement.clientWidth)
                    );
                }

                return isInViewport(arguments[0]);
            ", element);

            return isInViewport ?? false;
        }
    }
}
