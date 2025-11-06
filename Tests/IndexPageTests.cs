using Core.Driver;
using Core.Enum;
using WebPages;
namespace Tests
{
    [TestFixture(BrowserType.Chrome)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class IndexPageTests
    {
        private readonly BrowserType _browser;
        private IndexPage? _indexPage;

        public IndexPageTests(BrowserType browserType)
        {
            _browser = browserType;
        }

        [SetUp]
        public void SetUp()
        {
            DriverManager.Instance.Start(_browser);
            _indexPage = new IndexPage(DriverManager.Instance.Driver);
        }

        [Test]
        public void SearchAutomationLongVersion()
        {
            _indexPage!.Open().OpenSearchPanel().SearchFor("Automation");
            Assert.That(_indexPage.IsSearchResultsPageOpened("Automation"), Is.True, "Search results page did not load correctly.");
        }

        [Test]
        public void InsightsButtonPressed_WentToIsnightsPage()
        {
            _indexPage!.Open().ClickInsightsButton();
            Assert.That(_indexPage.IsInsightsPageOpened(), Is.True, "Insights page URL is not correct.");
        }

        [Test]
        public void ServiceButtonPressed_WentToServicePage()
        {
            _indexPage!.Open().ClickServicesButton();
            Assert.That(_indexPage.IsServicePageOpened(), Is.True, "Service page identifier is not displayed.");
        }

        [Test]
        public void SearchingBlockChain_WentToBlockChainSearchPage()
        {
            _indexPage!.Open().OpenSearchPanel().SearchFor("Blockchain");
            Assert.That(_indexPage.IsSearchResultsPageOpened("Blockchain"), Is.True, "Search results page did not load correctly.");
        }

        [TearDown]
        public void TearDown()
        {
            DriverManager.Instance.Quit();
        }
    }
}