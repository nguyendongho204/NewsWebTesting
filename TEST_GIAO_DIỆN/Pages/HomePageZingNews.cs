using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumTests.Helpers;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object cho trang chủ Zing News
    /// </summary>
    public class HomePageZingNews
    {
        private IWebDriver driver;
        private WebElementHelper helper;

        // Locators
        private By searchBox = By.XPath("//input[contains(@placeholder, 'Tìm kiếm')]");
        private By searchButton = By.XPath("//button[@type='submit']");
        private By categoryLinks = By.XPath("//nav//a[@href]");

        public HomePageZingNews(IWebDriver driver)
        {
            this.driver = driver;
            this.helper = new WebElementHelper(driver);
        }

        /// <summary>
        /// Navigate tới trang chủ Zing News
        /// </summary>
        public void Navigate()
        {
            driver.Navigate().GoToUrl("https://zingnews.vn/");
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// Tìm kiếm bài viết
        /// </summary>
        public void SearchArticle(string keyword)
        {
            helper.Click(searchBox);
            helper.SendText(searchBox, keyword);
            helper.Click(searchButton);
            System.Threading.Thread.Sleep(3000);
        }

        /// <summary>
        /// Kiểm tra search box visible
        /// </summary>
        public bool IsSearchBoxVisible()
        {
            return helper.IsElementVisible(searchBox);
        }

        /// <summary>
        /// Lấy danh sách category
        /// </summary>
        public IList<string> GetCategories()
        {
            var categories = new List<string>();
            var elements = helper.GetElements(categoryLinks);
            
            foreach (var element in elements.Take(10))
            {
                var text = element.Text;
                if (!string.IsNullOrEmpty(text))
                    categories.Add(text);
            }
            
            return categories;
        }

        /// <summary>
        /// Select category
        /// </summary>
        public void SelectCategory(string categoryName)
        {
            var categoryLink = By.XPath($"//nav//a[contains(text(), '{categoryName}')]");
            helper.Click(categoryLink);
            System.Threading.Thread.Sleep(3000);
        }
    }
}
