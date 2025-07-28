using CrossCutting.Static;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Business.PageObjects
{
    public abstract class BasePage
    {
        protected bool headless;
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        protected void ScrollTo(IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        protected bool WaitForElementToBeVisible(By by, int timeoutInSeconds = Constants.WaitTimeInSeconds)
        {
            return WaitFor(driver => driver.FindElement(by).Displayed, timeoutInSeconds);
        }

        protected bool WaitForElementToBeInViewport(IWebElement element, int timeoutInSeconds = Constants.WaitTimeInSeconds)
        {
            return WaitFor(driver => IsInViewport(element), timeoutInSeconds);
        }

        protected bool WaitFor(Func<IWebDriver, bool> condition, int timeoutInSeconds = Constants.WaitTimeInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(condition);
        }

        protected void SwipeDragging(By elementBy)
        {
            WaitForElementToBeVisible(elementBy);
            var carouselContainer = driver.FindElement(elementBy);

            new Actions(driver)
                .DragAndDropToOffset(carouselContainer, -300, 0)
                .Perform();
        }

        protected void SwipeMoving(By elementBy)
        {
            WaitForElementToBeVisible(elementBy);
            var carouselContainer = driver.FindElement(elementBy);

            new Actions(driver)
                .ClickAndHold(carouselContainer)
                .MoveByOffset(-300, 0)
                .Release()
                .Perform();
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
