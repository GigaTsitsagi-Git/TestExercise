
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebPages
{
    public class IndexPage
    {
        public static string Url { get; } = "https://www.epam.com";

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        //locators
        private readonly By _servicesButton = By.LinkText("Services");
        private readonly By _insightsButton = By.LinkText("Insights");
        private readonly By _searchButton = By.ClassName("header-search__button");
        private readonly By _searchPanel = By.ClassName("header-search__panel");
        private readonly By _searchInput = By.Name("q");
        private readonly By _findButton = By.XPath(".//button[contains(@class, 'custom-button') and contains(@class, 'custom-search-button')]");


        public IndexPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public IndexPage Open()
        {
            _driver.Url = Url;
            return this;
        }

        public IndexPage ClickInsightsButton()
        {
            _wait.Until(driver => driver.FindElement(_insightsButton)).Click();
            return this;
        }

        public IndexPage ClickServicesButton()
        {
            _wait.Until(driver => driver.FindElement(_servicesButton)).Click();
            return this;
        }

        public IndexPage OpenSearchPanel()
        {
            _wait.Until(driver => driver.FindElement(_searchButton)).Click();
            return this;
        }

        public IndexPage SearchFor(string text)
        {
            var searchPanel = _wait.Until(driver =>
            {
                var panel = driver.FindElement(_searchPanel);
                return panel.Displayed ? panel : null;
            });

            var searchInput = _wait.Until(driver =>
            {
                var input = searchPanel.FindElement(_searchInput);
                return input.Displayed && input.Enabled ? input : null;
            });
            searchInput.Clear();
            searchInput.SendKeys(text);
            _wait.Until(driver => driver.FindElement(_findButton)).Click();
            return this;
        }

        public bool IsServicePageOpened()
        {
            return _wait.Until(driver => driver.Url.Contains("/services"));
        }

        public bool IsInsightsPageOpened()
        {
            return _wait.Until(driver => driver.Url.Contains("/insights"));
        }

        public bool IsSearchResultsPageOpened(string text)
        {
            return _wait.Until(driver => driver.Url.Contains($"search?q={text}"));
        }
    }
}
