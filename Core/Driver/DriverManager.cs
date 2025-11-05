using Core.Enum;
using Core.Factory;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Driver
{
    //threadlocal singleton pattern
    public sealed class DriverManager
    {
        private static readonly ThreadLocal<IWebDriver?> _driver = new ThreadLocal<IWebDriver?>();
        private static readonly Lazy<DriverManager> _instance = new Lazy<DriverManager>(() => new DriverManager());

        private DriverManager() { }

        public static DriverManager Instance => _instance.Value;

        public IWebDriver Driver =>
            _driver.Value ?? throw new InvalidOperationException("WebDriver not started!");

        public IWebDriver Start(BrowserType browser)
        {
            if (_driver.Value == null)
            {
                _driver.Value = WebDriverFactory.Create(browser);
            }
            return _driver.Value;
        }

        public void Quit()
        {
            if (_driver.IsValueCreated && _driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value.Dispose();
                _driver.Value = null;
            }
        }
    }

    // singleton pattern without ThreadLocal
    /*public sealed class DriverManager
    {
        private static DriverManager? _instance;
        private static readonly object _lock = new object();
        private IWebDriver? _driver;

        private DriverManager() { }

        public static DriverManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new DriverManager();
                }
            }
        }

        public IWebDriver Driver
        {
            get => _driver ?? throw new InvalidOperationException("WebDriver not started!");
        }

        public void Start(BrowserType browser)
        {
            _driver ??= WebDriverFactory.Create(browser);
        }

        public void Quit()
        {
            _driver?.Quit();
            _driver = null;
            _instance = null;
        }
    }*/
}
