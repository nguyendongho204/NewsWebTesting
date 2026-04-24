using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Collections.Generic;

namespace SeleniumTests.Helpers
{
    /// <summary>
    /// Lớp hỗ trợ các hành động chung trên web (click, type, get text...)
    /// </summary>
    public class WebElementHelper
    {
        private IWebDriver driver;

        // Constructor - nhận vào driver
        public WebElementHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Nhập text vào ô input
        // Tham số: locator = định vị trí element, text = nội dung cần nhập
        public void SendText(By locator, string text)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Chờ element visible
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            // Tìm element và nhập text
            driver.FindElement(locator).SendKeys(text);
        }

        // Click vào element
        public void Click(By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Chờ element có thể click được
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            // Click vào element
            driver.FindElement(locator).Click();
        }

        // Kiểm tra element có hiển thị không
        // Trả về: true nếu visible, false nếu không
        public bool IsElementVisible(By locator)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
                return true;
            }
            catch
            {
                // Nếu timeout hoặc lỗi thì trả về false
                return false;
            }
        }

        // Lấy text từ element
        // Trả về: chuỗi text của element
        public string GetText(By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            return driver.FindElement(locator).Text;
        }

        // Lấy danh sách các element khớp với locator
        // Trả về: danh sách IWebElement
        public IList<IWebElement> GetElements(By locator)
        {
            return driver.FindElements(locator);
        }

        // Chờ element biến mất (invisible)
        public void WaitForElementInvisible(By locator, int seconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        // Cuộn trang để element xuất hiện trên màn hình
        public void ScrollToElement(By locator)
        {
            var element = driver.FindElement(locator);
            // Dùng JavaScript để scroll
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}
