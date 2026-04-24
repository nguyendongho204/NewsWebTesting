using OpenQA.Selenium;
using SeleniumTests.Helpers;
using System.Collections.Generic;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object cho trang kết quả tìm kiếm Zing News
    /// </summary>
    public class SearchResultsPageZingNews
    {
        private IWebDriver driver;
        private WebElementHelper helper;

        // Locators
        private By searchResults = By.XPath("//h3//a | //h2//a");
        private By noResultsMessage = By.XPath("//*[contains(text(), 'không tìm thấy') or contains(text(), 'Không có kết quả')]");

        public SearchResultsPageZingNews(IWebDriver driver)
        {
            this.driver = driver;
            this.helper = new WebElementHelper(driver);
        }

        /// <summary>
        /// Kiểm tra có kết quả tìm kiếm
        /// </summary>
        public bool HasSearchResults()
        {
            return helper.GetElements(searchResults).Count > 0;
        }

        /// <summary>
        /// Lấy danh sách kết quả tìm kiếm
        /// </summary>
        public IList<string> GetSearchResultTitles()
        {
            var titles = new List<string>();
            var elements = helper.GetElements(searchResults);
            
            foreach (var element in elements)
            {
                var title = element.Text;
                if (!string.IsNullOrEmpty(title))
                    titles.Add(title);
            }
            
            return titles;
        }

        /// <summary>
        /// Click bài viết đầu tiên
        /// </summary>
        public void ClickFirstArticle()
        {
            var results = helper.GetElements(searchResults);
            if (results.Count > 0)
            {
                results[0].Click();
                System.Threading.Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// Click bài viết theo index
        /// </summary>
        public void ClickArticleByIndex(int index)
        {
            var results = helper.GetElements(searchResults);
            if (results.Count > index)
            {
                results[index].Click();
                System.Threading.Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// Kiểm tra có thông báo không có kết quả
        /// </summary>
        public bool HasNoResultsMessage()
        {
            return helper.IsElementVisible(noResultsMessage);
        }
    }
}
