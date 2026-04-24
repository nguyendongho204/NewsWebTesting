# CÁCH SỬA VÀ CHẠY CÁC TESTS

## 🔧 SỬA LỖI KHÔNG KHỚP PHIÊN BẢN BROWSER-DRIVER

Hiện tại: Chrome 147 vs ChromeDriver 131  
Cần sửa một trong ba cách sau:

### **Cách 1: Hạ Cấp Chrome Browser (ĐƯỢC KHUYẾN NGHỊ)**
```powershell
# Hạ Chrome xuống phiên bản 131 từ trang Google Chrome
# hoặc sử dụng Windows package manager (nếu có)
winget install Google.Chrome --version 131.0.6778.0
```

### **Cách 2: Nâng Cấp Gói Chromedriver**
```bash
cd TEST_GIAO_DIỆN
dotnet add package Selenium.WebDriver.ChromeDriver --version 147.0.0
dotnet restore
dotnet build
```

### **Cách 3: Thêm WebDriverManager (TỰ ĐỘNG)**
```bash
cd TEST_GIAO_DIỆN
dotnet add package WebDriverManager --version 2.17.0
```

Sau đó sửa `DriverHelper.cs`:
```csharp
using WebDriverManager;
using WebDriverManager.DriverManager;

public static IWebDriver InitDriver()
{
    new DriverManager().SetUpDriver(new ChromeDriver());
    var driver = new ChromeDriver();
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    return driver;
}
```

---

## ✅ CHẠY CÁC TESTS

### **1. Chạy Tests Giao Diện (Selenium)**
```bash
cd TEST_GIAO_DIỆN

# Build dự án
dotnet build

# Chạy tests
dotnet test --logger "console;verbosity=detailed"

# Chạy test class cụ thể
dotnet test --filter "ClassName=VnExpressSearchTests"
```

**Kỳ vọng:** 16 tests chạy (nếu đã sửa khớp browser-driver)

---

### **2. Chạy Tests Logic (NUnit)**
```bash
cd TEST_LOGIC

# Build dự án
dotnet restore
dotnet build

# Chạy tests (không có phụ thuộc browser)
dotnet test --logger "console;verbosity=detailed"
```

**Kỳ vọng:** 19 tests - toàn bộ PASS

---

### **3. Chạy Performance Tests (JMeter)**

Trước tiên cài JMeter:
```bash
# Windows
choco install jmeter

# hoặc download từ https://jmeter.apache.org/download_jmeter.html
```

Chạy test:
```bash
cd JMeterTests

# Chạy performance test
jmeter -n -t NewsWebsite_PerformanceTest.jmx \
        -l results.jtl \
        -j jmeter.log \
        -e -o report_output

# Xem kết quả
# Report sẽ tạo HTML tại: report_output/index.html
```

---

## 📊 EXPECTED TEST RESULTS

### UI Tests (Selenium) - 16 tests
```
✅ VnExpressSearchTests: 6 tests
   - TestSearchWithValidKeyword
   - TestSearchWithEmptyKeyword
   - TestSearchWithLongKeyword
   - TestSearchWithSpecialCharacters
   - TestSearchResultsContainKeyword
   - TestClickFirstArticleInSearchResults

✅ VnExpressCategoryFilterTests: 3 tests
   - TestSelectCategoryFromMenu
   - TestCategoryListIsDisplayed
   - TestCategoryURLChange

✅ VnExpressArticleDetailTests: 4 tests
   - TestViewArticleDetailAfterSearch
   - TestArticleTitleIsNotEmpty
   - TestArticleContentIsNotEmpty
   - TestArticleContentContainsSearchKeyword

✅ ZingNewsTests: 3 tests
   - TestZingNewsSearch
   - TestZingNewsCategoryMenu
   - TestZingNewsArticleClick

Status: Phụ thuộc vào website status (có thể fail nếu website down)
```

### Logic Tests (NUnit) - 19 tests
```
✅ SearchValidatorTests: 10 tests - Expected: ALL PASS
✅ SearchServiceTests: 9 tests - Expected: ALL PASS

Status: SHOULD ALL PASS (không phụ thuộc website)
```

### Performance Tests (JMeter) - 3 scenarios
```
✅ VnExpress Normal Load (500 users)
✅ Zing News Normal Load (500 users)
✅ VnExpress Stress Test (1000 users)

Metrics: Response time, throughput, error rate
```

---

## 🐛 TROUBLESHOOTING

### Lỗi: "ChromeDriver not found"
```
Solution: 
1. cd SeleniumTests
2. dotnet restore
3. rm -r bin obj
4. dotnet build
```

### Lỗi: "session not created: version mismatch"
```
Solution: Fix bằng một trong ba cách ở trên
```

### Lỗi: "Implicit wait timeout"
```
Solution: Tăng ImplicitWait trong DriverHelper.cs
   driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
```

### Tests không tìm thấy elements
```
Solution: 
1. Website URL có thay đổi? Cập nhật URL trong Pages
2. CSS Selectors có thay đổi? Update trong Page Objects
3. Internet disconnect? Kiểm tra kết nối
```

---

## 📝 GENERATE TEST REPORTS

### NUnit Report:
```bash
cd SeleniumTests
dotnet test --logger "trx;LogFileName=TestResults.trx"

# Hoặc
dotnet test --logger "html;LogFileName=TestResults.html"
```

### JMeter Report:
JMeter tự động tạo HTML report:
```bash
# Report sẽ ở: report_output/index.html
```

---

## 🚀 CI/CD SETUP (Optional)

### GitHub Actions:
```yaml
name: Run Tests

on: [push, pull_request]

jobs:
  test:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v2
      - uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '6.0.x'
      - run: dotnet test SeleniumTests/SeleniumTests.csproj
      - run: dotnet test NUnitTests/NUnitTests.csproj
```

---

## ✅ CHECKLIST TRƯỚC KHI RUN TESTS

- [ ] .NET SDK 6.0+ đã cài
- [ ] Chrome Browser cài đặt (version khớp với ChromeDriver)
- [ ] ChromeDriver tải về hoặc đã resolve package
- [ ] Internet kết nối bình thường
- [ ] VnExpress.com và ZingNews.vn truy cập được
- [ ] Không có proxy hoặc firewall chặn
- [ ] RAM đủ (recommendation: >2GB)
- [ ] Headless mode disabled (UI tests cần browser window)

---

## 📞 SUPPORT

Nếu có vấn đề:
1. Kiểm tra BUILD STATUS: `dotnet build`
2. Kiểm tra COMPILATION: `dotnet build --verbosity diagnostic`
3. Xem DETAILED ERRORS: `dotnet test --logger "console;verbosity=detailed"`
4. Check BROWSER: Mở Chrome → Xem version (Settings → About Chrome)
5. Check CHROMEDRIVER: `chromedriver --version`

---

**Status:** Ready for execution  
**Last Updated:** 2026-04-16  
**Project:** QA Testing - VnExpress & Zing News
