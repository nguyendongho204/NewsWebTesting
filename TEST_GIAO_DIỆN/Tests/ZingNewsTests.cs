using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using SeleniumTests.Helpers;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    /// <summary>
    /// Test case cho Zing News
    /// </summary>
    [TestFixture]
    public class ZingNewsTests
    {
        private IWebDriver driver = null!;
        private HomePageZingNews homePage = null!;

        [SetUp]
        public void Setup()
        {
            driver = DriverHelper.InitDriver();
            homePage = new HomePageZingNews(driver);
        }

        [TearDown]
        public void TearDown()
        {
            DriverHelper.CloseDriver();
        }

        /// <summary>
        /// TC014_ZING: Tìm kiếm bài viết trên Zing News
        /// Input: keyword = "Sports"
        /// Expected: Hiển thị kết quả tìm kiếm
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestZingNewsSearch()
        {
            // Arrange
            homePage.Navigate();
            Assert.That(homePage.IsSearchBoxVisible(), Is.True, "Search box should be visible");

            // Act
            homePage.SearchArticle("Thể thao");

            // Assert
            System.Threading.Thread.Sleep(2000);
            var searchPage = new SearchResultsPageZingNews(driver);
            Assert.That(searchPage.HasSearchResults(), Is.True, "Search results should be displayed");
        }

        /// <summary>
        /// TC015_ZING: Kiểm tra category menu trên Zing News
        /// Input: Load trang chủ
        /// Expected: Danh sách category xuất hiện
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestZingNewsCategoryMenu()
        {
            // Arrange & Act
            homePage.Navigate();
            var categories = homePage.GetCategories();

            // Assert
            Assert.That(categories.Count, Is.GreaterThan(0), "Should have categories");
        }

        /// <summary>
        /// TC016_ZING: Click bài viết đầu tiên từ kết quả tìm kiếm
        /// Input: Search and click first article
        /// Expected: Article detail page loaded
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestZingNewsArticleClick()
        {
            // Arrange
            homePage.Navigate();
            homePage.SearchArticle("News");

            // Act
            var searchPage = new SearchResultsPageZingNews(driver);
            if (searchPage.HasSearchResults())
            {
                searchPage.ClickFirstArticle();

                // Assert
                System.Threading.Thread.Sleep(3000);
                Assert.That(driver.Url.Contains("search"), Is.Not.EqualTo(true), "Should navigate to article page");
            }
        }
    }
}
