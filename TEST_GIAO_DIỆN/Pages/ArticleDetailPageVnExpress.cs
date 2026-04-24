using System;
using OpenQA.Selenium;
using SeleniumTests.Helpers;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object cho trang chi tiết bài viết VnExpress
    /// </summary>
    public class ArticleDetailPageVnExpress
    {
        private IWebDriver driver;
        private WebElementHelper helper;

        // Locators
        private By articleTitle = By.XPath("//h1[@class='title-detail']");
        private By articleContent = By.XPath("//article | //div[@class='content_detail']");
        private By publishDate = By.XPath("//span[@class='date']");
        private By authorName = By.XPath("//span[@class='author-name']");

        public ArticleDetailPageVnExpress(IWebDriver driver)
        {
            this.driver = driver;
            this.helper = new WebElementHelper(driver);
        }

        /// <summary>
        /// Kiểm tra bài viết đã load không
        /// </summary>
        public bool IsArticleLoaded()
        {
            return helper.IsElementVisible(articleTitle);
        }

        /// <summary>
        /// Lấy tiêu đề bài viết
        /// </summary>
        public string GetArticleTitle()
        {
            return helper.GetText(articleTitle);
        }

        /// <summary>
        /// Lấy nội dung bài viết
        /// </summary>
        public string GetArticleContent()
        {
            return helper.GetText(articleContent);
        }

        /// <summary>
        /// Lấy ngày đăng bài
        /// </summary>
        public string GetPublishDate()
        {
            return helper.GetText(publishDate);
        }

        /// <summary>
        /// Kiểm tra nội dung bài viết có chứa keyword không
        /// </summary>
        public bool ContentContainsKeyword(string keyword)
        {
            var content = GetArticleContent();
            return content.Contains(keyword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
