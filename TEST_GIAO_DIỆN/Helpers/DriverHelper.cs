using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace SeleniumTests.Helpers
{
    /// <summary>
    /// Lớp quản lý WebDriver - khởi tạo, lấy, và đóng Firefox/Chrome
    /// </summary>
    public class DriverHelper
    {
        private static IWebDriver? driver;

        // Khởi tạo và trả về WebDriver mới
        public static IWebDriver InitDriver()
        {
            // Cài đặt tùy chọn cho Chrome
            var options = new ChromeOptions();
            options.AddArgument("--disable-notifications");  // Tắt thông báo
            options.AddArgument("--disable-popup-blocking");  // Tắt chặn popup
            options.AddArgument("--disable-blink-features=AutomationControlled");  // Tắt cảnh báo automation
            
            // Tạo driver mới
            driver = new ChromeDriver(options);
            
            // Đặt thời gian chờ mặc định là 10 giây
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            return driver;
        }

        // Lấy driver hiện tại đang sử dụng
        public static IWebDriver GetDriver()
        {
            // Nếu chưa khởi tạo thì ném lỗi
            if (driver == null)
                throw new NullReferenceException("Chưa khởi tạo Driver!");
            return driver;
        }

        // Đóng driver và giải phóng tài nguyên
        public static void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();  // Đóng trình duyệt
                driver = null;  // Xóa reference
            }
        }

        // Tạo đối tượng chờ đợi (explicit wait)
        public static WebDriverWait GetWait(int seconds = 10)
        {
            return new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(seconds));
        }
    }
}
