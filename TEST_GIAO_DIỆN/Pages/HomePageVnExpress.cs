using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumTests.Helpers;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object cho trang chủ VnExpress
    /// Chứa các method để interact với trang chủ
    /// </summary>
    public class HomePageVnExpress
    {
        private IWebDriver driver;
        private WebElementHelper helper;

        // Định vị các element trên trang
        private By searchBox = By.XPath("//input[@placeholder='Tìm kiếm']");
        private By searchButton = By.XPath("//button[contains(@class, 'search_button')]");
        private By categoryMenu = By.XPath("//nav//a[@href]");

        // Constructor
        public HomePageVnExpress(IWebDriver driver)
        {
            this.driver = driver;
            this.helper = new WebElementHelper(driver);
        }

        // Truy cập vào trang chủ VnExpress
        public void Navigate()
        {
            driver.Navigate().GoToUrl("https://vnexpress.net/");
            System.Threading.Thread.Sleep(2000); // Chờ trang load xong
        }

        // Tìm kiếm bài viết theo keyword
        // Tham số: keyword = từ khoá tìm kiếm
        public void SearchArticle(string keyword)
        {
            helper.Click(searchBox);        // Click vào ô tìm kiếm
            helper.SendText(searchBox, keyword);  // Nhập từ khoá
            helper.Click(searchButton);     // Nhấn nút tìm kiếm
            System.Threading.Thread.Sleep(3000); // Chờ kết quả load
        }

        // Kiểm tra ô tìm kiếm có hiển thị không
        // Trả về: true nếu có, false nếu không
        public bool IsSearchBoxVisible()
        {
            return helper.IsElementVisible(searchBox);
        }

        // Lấy danh sách các chuyên mục từ menu
        // Trả về: List các tên chuyên mục
        public IList<string> GetCategories()
        {
            var categories = new List<string>();
            var elements = helper.GetElements(categoryMenu);
            
            // Duyệt qua 10 category đầu tiên
            foreach (var element in elements.Take(10))
            {
                var text = element.Text;
                if (!string.IsNullOrEmpty(text))
                    categories.Add(text);
            }
            
            return categories;
        }

        // Chọn một chuyên mục từ menu
        // Tham số: categoryName = tên chuyên mục muốn chọn
        public void SelectCategory(string categoryName)
        {
            var categoryLink = By.XPath($"//nav//a[contains(text(), '{categoryName}')]");
            helper.Click(categoryLink);
            System.Threading.Thread.Sleep(3000); // Chờ trang chuyên mục load
        }
    }
}
