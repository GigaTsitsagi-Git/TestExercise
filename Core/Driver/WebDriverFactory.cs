using Core.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.Factory
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(BrowserType browsertype)
        {
            IWebDriver driver = browsertype switch
            {
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Firefox => CreateFirefoxDriver(),
                BrowserType.Edge => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Unsupported browser: {browsertype}")
            };
            driver.Manage().Window.Maximize();

            return driver;
        }

        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            return new EdgeDriver(options);
        }
    }
}
