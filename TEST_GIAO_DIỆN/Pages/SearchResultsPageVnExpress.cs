using OpenQA.Selenium;
using SeleniumTests.Helpers;
using System.Collections.Generic;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object cho trang kết quả tìm kiếm VnExpress
    /// </summary>
    public class SearchResultsPageVnExpress
    {
        private IWebDriver driver;
        private WebElementHelper helper;

        // Định vị các element trên trang kết quả
        private By searchResults = By.XPath("//h3[@class='title-news']//a | //h2[@class='title-news']//a");
        private By noResultsMessage = By.XPath("//*[contains(text(), 'không tìm thấy')]");

        public SearchResultsPageVnExpress(IWebDriver driver)
        {
            this.driver = driver;
            this.helper = new WebElementHelper(driver);
        }

        // Kiểm tra có kết quả tìm kiếm không
        // Trả về: true nếu có kết quả, false nếu không
        public bool HasSearchResults()
        {
            return helper.GetElements(searchResults).Count > 0;
        }

        // Lấy danh sách tiêu đề bài viết từ kết quả tìm kiếm
        // Trả về: List các tiêu đề bài viết
        public IList<string> GetSearchResultTitles()
        {
            var titles = new List<string>();
            var elements = helper.GetElements(searchResults);
            
            // Duyệt qua tất cả bài viết
            foreach (var element in elements)
            {
                var title = element.Text;
                if (!string.IsNullOrEmpty(title))
                    titles.Add(title);
            }
            
            return titles;
        }

        // Click vào bài viết đầu tiên trong danh sách
        public void ClickFirstArticle()
        {
            var results = helper.GetElements(searchResults);
            if (results.Count > 0)
            {
                results[0].Click();
                System.Threading.Thread.Sleep(3000); // Chờ trang bài viết load
            }
        }

        // Click vào bài viết theo vị trí
        // Tham số: index = vị trí bài viết (0 = bài đầu)
        public void ClickArticleByIndex(int index)
        {
            var results = helper.GetElements(searchResults);
            if (results.Count > index)
            {
                results[index].Click();
                System.Threading.Thread.Sleep(3000);
            }
        }

        // Kiểm tra có thông báo "không tìm thấy" không
        // Trả về: true nếu không có kết quả, false nếu có
        public bool HasNoResultsMessage()
        {
            return helper.IsElementVisible(noResultsMessage);
        }
    }
}
