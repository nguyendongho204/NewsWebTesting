using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using SeleniumTests.Helpers;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    /// <summary>
    /// Test tính năng tìm kiếm trên VnExpress
    /// </summary>
    [TestFixture]
    public class VnExpressSearchTests
    {
        private IWebDriver driver = null!;
        private HomePageVnExpress homePage = null!;
        private SearchResultsPageVnExpress searchResultsPage = null!;

        // Chạy trước mỗi test
        [SetUp]
        public void Setup()
        {
            driver = DriverHelper.InitDriver();
            homePage = new HomePageVnExpress(driver);
            searchResultsPage = new SearchResultsPageVnExpress(driver);
        }

        // Chạy sau mỗi test
        [TearDown]
        public void TearDown()
        {
            DriverHelper.CloseDriver();
        }

        /// <summary>
        /// TEST 1: Tìm kiếm với keyword hợp lệ
        /// Input: keyword = "Vietnam"
        /// Kỳ vọng: Hiển thị danh sách bài viết
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestSearchWithValidKeyword()
        {
            // Bước 1: Truy cập trang chủ
            homePage.Navigate();
            Assert.That(homePage.IsSearchBoxVisible(), Is.True, "Ô tìm kiếm phải hiển thị");

            // Bước 2: Tìm kiếm "Vietnam"
            homePage.SearchArticle("Vietnam");

            // Bước 3: Kiểm tra kết quả
            Assert.That(searchResultsPage.HasSearchResults(), Is.True, "Phải có kết quả tìm kiếm");
            var results = searchResultsPage.GetSearchResultTitles();
            Assert.That(results.Count, Is.GreaterThan(0), "Phải có ít nhất 1 kết quả");
        }

        /// <summary>
        /// TEST 2: Tìm kiếm với keyword rỗng
        /// Input: keyword = ""
        /// Kỳ vọng: Không hiển thị kết quả hoặc báo lỗi
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestSearchWithEmptyKeyword()
        {
            // Bước 1: Truy cập trang chủ
            homePage.Navigate();

            // Bước 2: Tìm kiếm với keyword rỗng
            homePage.SearchArticle("");

            // Bước 3: Chờ và kiểm tra
            System.Threading.Thread.Sleep(2000);
            bool hasNoResults = !searchResultsPage.HasSearchResults() || searchResultsPage.HasNoResultsMessage();
            Assert.That(hasNoResults, Is.True, "Keyword rỗng không nên trả về kết quả");
        }

        /// <summary>
        /// TEST 3: Tìm kiếm với keyword rất dài (>100 ký tự)
        /// Input: 150 ký tự 'a'
        /// Kỳ vọng: Hệ thống xử lý mà không crash
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestSearchWithLongKeyword()
        {
            // Bước 1: Truy cập trang chủ
            homePage.Navigate();
            
            // Bước 2: Tạo keyword dài 150 ký tự
            var longKeyword = new string('a', 150);

            // Bước 3: Tìm kiếm
            homePage.SearchArticle(longKeyword);

            // Bước 4: Kiểm tra hệ thống không crash
            System.Threading.Thread.Sleep(2000);
            Assert.That(driver.Title, Is.Not.Null, "Trang vẫn phải tồn tại");
        }

        /// <summary>
        /// TEST 4: Tìm kiếm với ký tự đặc biệt
        /// Input: keyword = "!@#$%"
        /// Kỳ vọng: Không trả về kết quả
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestSearchWithSpecialCharacters()
        {
            // Bước 1: Truy cập trang chủ
            homePage.Navigate();

            // Bước 2: Tìm kiếm với ký tự đặc biệt
            homePage.SearchArticle("!@#$%");

            // Bước 3: Chờ kết quả
            System.Threading.Thread.Sleep(2000);
            bool hasNoResultsOrError = !searchResultsPage.HasSearchResults() || searchResultsPage.HasNoResultsMessage();
            Assert.That(hasNoResultsOrError, Is.True, "Ký tự đặc biệt không nên trả về kết quả");
        }

        /// <summary>
        /// TEST 5: Kiểm tra kết quả chứa keyword tìm kiếm
        /// Input: keyword = "Kinh tế"
        /// Kỳ vọng: Tất cả kết quả đều liên quan đến keyword
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestSearchResultsContainKeyword()
        {
            // Bước 1: Truy cập trang chủ
            homePage.Navigate();
            var keyword = "Kinh tế";

            // Bước 2: Tìm kiếm
            homePage.SearchArticle(keyword);

            // Bước 3: Kiểm tra kết quả
            var results = searchResultsPage.GetSearchResultTitles();
            Assert.That(results.Count, Is.GreaterThan(0), "Phải có kết quả tìm kiếm");
            
            // Bước 4: Kiểm tra ít nhất một kết quả chứa keyword
            bool hasKeyword = results.Any(r => r.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            Assert.That(hasKeyword, Is.True, "Ít nhất 1 kết quả phải chứa keyword");
        }

        /// <summary>
        /// TEST 6: Click vào bài viết đầu tiên trong kết quả
        /// Input: Click bài viết thứ 1
        /// Kỳ vọng: Trang chi tiết bài viết load thành công
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestClickFirstArticleInSearchResults()
        {
            // Bước 1: Tìm kiếm
            homePage.Navigate();
            homePage.SearchArticle("News");
            Assert.That(searchResultsPage.HasSearchResults(), Is.True, "Phải có kết quả");

            // Bước 2: Click bài viết đầu
            searchResultsPage.ClickFirstArticle();

            // Bước 3: Kiểm tra bài viết đã mở
            System.Threading.Thread.Sleep(2000);
            var articlePage = new ArticleDetailPageVnExpress(driver);
            Assert.That(articlePage.IsArticleLoaded(), Is.True, "Trang bài viết phải load");
        }
    }
}
