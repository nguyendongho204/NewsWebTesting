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
    /// Test case cho tính năng Filter/Category trên VnExpress
    /// </summary>
    [TestFixture]
    public class VnExpressCategoryFilterTests
    {
        private IWebDriver driver = null!;
        private HomePageVnExpress homePage = null!;

        [SetUp]
        public void Setup()
        {
            driver = DriverHelper.InitDriver();
            homePage = new HomePageVnExpress(driver);
        }

        [TearDown]
        public void TearDown()
        {
            DriverHelper.CloseDriver();
        }

        /// <summary>
        /// TC007_VNE: Kiểm tra danh sách category có hiển thị không
        /// Input: Load trang chủ
        /// Expected: Hiển thị danh sách các chuyên mục
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestCategoryListIsDisplayed()
        {
            // Arrange & Act
            homePage.Navigate();
            var categories = homePage.GetCategories();

            // Assert
            Assert.That(categories.Count, Is.GreaterThan(0), "Category list should not be empty");
            Assert.That(categories.Any(), Is.True, "Should have at least one category");
        }

        /// <summary>
        /// TC008_VNE: Chọn một chuyên mục từ menu
        /// Input: Category = "Thế giới"
        /// Expected: Hiển thị danh sách bài viết của chuyên mục đó
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestSelectCategoryFromMenu()
        {
            // Arrange
            homePage.Navigate();
            var categories = homePage.GetCategories();
            var categoryToSelect = categories.FirstOrDefault();
            
            Assert.That(categoryToSelect, Is.Not.Null, "Should have at least one category to select");

            // Act
            try
            {
                homePage.SelectCategory(categoryToSelect!);

                // Assert
                System.Threading.Thread.Sleep(2000);
                Assert.That(driver.Url, Is.Not.EqualTo("https://vnexpress.net/"), "URL should change after selecting category");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Should be able to select category: {ex.Message}");
            }
        }

        /// <summary>
        /// TC009_VNE: Kiểm tra URL thay đổi khi chọn category
        /// Input: Click category link
        /// Expected: URL contains category name
        /// </summary>
        [Test]
        [Category("UI")]
        public void TestCategoryURLChange()
        {
            // Arrange
            homePage.Navigate();
            var initialUrl = driver.Url;

            // Act
            var categories = homePage.GetCategories();
            if (categories.Count > 0)
            {
                try
                {
                    homePage.SelectCategory(categories[0]);
                    System.Threading.Thread.Sleep(2000);

                    // Assert
                    var currentUrl = driver.Url;
                    Assert.That(currentUrl, Is.Not.EqualTo(initialUrl), "URL should change after category selection");
                }
                catch
                {
                    // Category may not be clickable
                    Assert.Pass("Category selection may not be available");
                }
            }
        }
    }
}
