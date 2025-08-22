using CrossCutting.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Business.PageObjects
{
    public abstract class BasePage
    {
        public ILogger logger;
        public IWebDriver driver;
        public IConfiguration testData;
        public IConfiguration configuration;

        private const int WaitTimeInSeconds = 10;

        protected BasePage(BasePage basePage)
        {
            this.driver = basePage.driver;
            this.logger = basePage.logger;
            this.testData = basePage.testData;
            this.configuration = basePage.configuration;
        }

        public BasePage(IWebDriver driver, ISimpleServiceProvider serviceProvider)
        {
            this.driver = driver;
            this.logger = serviceProvider.GetLogger<BasePage>();
            this.testData = serviceProvider.GetTestData();
            this.configuration = serviceProvider.GetConfiguration();
        }

        protected void Click(IWebElement element)
        {
            logger.LogDebug("Clicking element: {Element}", element);

            element.Click();
        }

        protected void SendKeys(IWebElement element, string textToWrite)
        {
            logger.LogDebug("Writing in element: {Element}. Content: {Content}", element, textToWrite);

            element.Clear();
            element.SendKeys(textToWrite);
        }

        protected void ScrollTo(IWebElement element)
        {
            logger.LogDebug("Scrolling to element: {Element}", element);

            var actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        protected bool WaitForElementToBeVisible(By by, int timeoutInSeconds = WaitTimeInSeconds)
        {
            return WaitFor(driver => driver.FindElement(by).Displayed, timeoutInSeconds);
        }

        protected bool WaitForElementToBeVisible(IWebElement element, int timeoutInSeconds = WaitTimeInSeconds)
        {
            return WaitFor(driver => element.Displayed, timeoutInSeconds);
        }

        protected bool WaitForElementToBeInViewport(IWebElement element, int timeoutInSeconds = WaitTimeInSeconds)
        {
            return WaitFor(driver => IsInViewport(element), timeoutInSeconds);
        }

        protected bool WaitFor(Func<IWebDriver, bool> condition, int timeoutInSeconds = WaitTimeInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(condition);
        }

        protected void SwipeDragging(By elementBy)
        {

            WaitForElementToBeVisible(elementBy);
            var carouselContainer = driver.FindElement(elementBy);
            logger.LogDebug("Swipping element: {Element}", carouselContainer);

            new Actions(driver)
                .DragAndDropToOffset(carouselContainer, -300, 0)
                .Perform();
        }

        protected void SwipeMoving(By elementBy)
        {
            WaitForElementToBeVisible(elementBy);
            var carouselContainer = driver.FindElement(elementBy);
            logger.LogDebug("Swipping element: {Element}", carouselContainer);

            new Actions(driver)
                .ClickAndHold(carouselContainer)
                .MoveByOffset(-300, 0)
                .Release()
                .Perform();
        }

        // TODO remove
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
