# 📚 HƯỚNG DẪN HOẠT ĐỘNG DỰ ÁN - NEWS WEBSITE TESTING

## 🎯 Mục đích Dự Án

Dự án này xây dựng một **"hệ thống kiểm tra chất lượng tự động"** cho 2 website tin tức lớn nhất Việt Nam:
- **VnExpress** (https://vnexpress.net/)
- **Zing News** (https://zingnews.vn/)

Mục tiêu: **Kiểm tra tự động** thay vì kiểm tra thủ công, tiết kiệm thời gian và đảm bảo chất lượng.

---

## 🏗️ CẤU TRÚC DỰ ÁN

```
NewsWebTesting/
│
├── 📂 SeleniumTests/          ← Test giao diện (UI Tests)
│   ├── Pages/                 ← Page Objects (mô tả các trang web)
│   ├── Tests/                 ← Test cases (kịch bản kiểm tra)
│   └── Helpers/               ← Công cụ trợ giúp
│
├── 📂 NUnitTests/             ← Test logic (Unit Tests)
│   ├── Tests/                 ← Các bài test kiểm tra xử lý dữ liệu
│   └── Utils/                 ← Công cụ xử lý dữ liệu
│
├── 📂 JMeterTests/            ← Test hiệu suất (Performance Tests)
│   └── NewsWebsite_PerformanceTest.jmx
│
└── 📂 Documentation/          ← Tài liệu báo cáo
```

---

## 🔄 GỒM 3 LOẠI TEST

### **1️⃣ UI TESTS (Selenium WebDriver) - Test Giao Diện** 🌐

**Mục đích:** Kiểm tra website hoạt động đúng từ góc độ người dùng

**Cách hoạt động:**
```
1. Mở browser Chrome tự động
2. Truy cập vào website
3. Tìm kiếm từ khóa
4. Click nút, scroll trang
5. Kiểm tra kết quả hiển thị
6. Đóng browser
```

**Ví dụ test:**
```csharp
[Test]
public void TestSearchWithValidKeyword()
{
    // Bước 1: Vào trang chủ VnExpress
    homePage.Navigate();
    
    // Bước 2: Tìm kiếm từ khóa "Vietnam"
    homePage.SearchArticle("Vietnam");
    
    // Bước 3: Kiểm tra xem có kết quả hay không
    Assert.IsTrue(searchResults.Count > 0);
}
```

**Test Cases (16 tests):**
- ✅ Tìm kiếm với từ khóa hợp lệ
- ✅ Tìm kiếm với từ khóa trống
- ✅ Tìm kiếm với ký tự đặc biệt
- ✅ Click vào bài viết
- ✅ Xem chi tiết bài viết
- ✅ Lọc theo danh mục

---

### **2️⃣ UNIT TESTS (NUnit) - Test Logic** 🧪

**Mục đích:** Kiểm tra logic xử lý dữ liệu bên trong hệ thống

**Cách hoạt động:**
```
Không cần browser, chỉ test các hàm/function pure:
1. Input dữ liệu đầu vào
2. Gọi hàm xử lý
3. Kiểm tra output có đúng không
```

**Ví dụ test:**
```csharp
[Test]
public void TestValidateKeyword()
{
    // Input: Keyword "Vietnam"
    var result = SearchValidator.ValidateKeyword("Vietnam");
    
    // Output mong đợi: true (hợp lệ)
    Assert.IsTrue(result);
}
```

**Test Coverage (19 tests):**
- ✅ Validate từ khóa rỗng
- ✅ Validate từ khóa dài
- ✅ Detect ký tự đặc biệt
- ✅ Chống SQL Injection
- ✅ Tìm kiếm không phân biệt hoa/thường

**Ưu điểm:** 
- Chạy SIÊU NHANH (< 1 giây)
- Luôn pass (không phụ thuộc website)
- Không cần internet

---

### **3️⃣ PERFORMANCE TESTS (JMeter) - Test Hiệu Suất** ⚡

**Mục đích:** Kiểm tra website chịu được bao nhiêu người dùng cùng lúc

**Cách hoạt động:**
```
1. Mô phỏng 100, 500, 1000 người dùng
2. Người dùng truy cập website cùng lúc
3. Theo dõi:
   - Response time (thời gian phản hồi)
   - Throughput (xử lý được bao nhiêu request)
   - Error rate (tỷ lệ lỗi)
```

**Scenario:**
- Nhẹ: 100 users
- Bình thường: 500 users
- Nặng: 1000 users

---

## 🔧 CÁC CÔNG CỤ & CÔNG NGHỆ DÙNG

| Tool | Phiên bản | Mục đích |
|------|----------|---------|
| **Selenium WebDriver** | 4.15.0 | Tự động hóa Chrome browser |
| **NUnit** | 4.0.1 | Framework để viết unit tests |
| **Apache JMeter** | 5.5 | Test tải/hiệu suất |
| **C# .NET** | 6.0 | Ngôn ngữ lập trình |
| **ChromeDriver** | 147.0 | Điều khiển Chrome |

---

## 📂 FILE QUAN TRỌNG

### SeleniumTests/
```
SeleniumTests/
├── Pages/
│   ├── HomePageVnExpress.cs      ← Mô tả trang chủ VnExpress
│   ├── SearchResultsPageVnExpress.cs
│   ├── ArticleDetailPageVnExpress.cs
│   ├── HomePageZingNews.cs
│   └── SearchResultsPageZingNews.cs
├── Tests/
│   ├── VnExpressSearchTests.cs    ← 6 test về tìm kiếm
│   ├── VnExpressCategoryFilterTests.cs
│   ├── VnExpressArticleDetailTests.cs
│   └── ZingNewsTests.cs
└── Helpers/
    ├── DriverHelper.cs            ← Khởi tạo Chrome WebDriver
    └── WebElementHelper.cs        ← Các hàm tương tác web element
```

### NUnitTests/
```
NUnitTests/
├── Tests/
│   ├── SearchValidatorTests.cs   ← 10 test về validation
│   └── SearchServiceTests.cs     ← 9 test về dịch vụ tìm kiếm
└── Utils/
    └── SearchValidator.cs        ← Logic xử lý dữ liệu
```

---

## 🚀 CÁCH CHẠY TEST

### **Cách 1: Chạy tất cả (Recommended)**

```powershell
# Mở PowerShell, di chuyển vào thư mục project
cd d:\Nam4\qlkt\NewsWebTesting

# Chạy script tối ưu
.\RUN_ALL_TESTS.ps1
```

### **Cách 2: Chạy riêng từng loại**

**Chạy UI Tests:**
```powershell
cd SeleniumTests
dotnet test
```

**Chạy Unit Tests:**
```powershell
cd NUnitTests
dotnet test
```

**Chạy Performance Tests:**
```powershell
cd JMeterTests
jmeter -n -t NewsWebsite_PerformanceTest.jmx \
        -l results.jtl \
        -e -o report_output
```

### **Cách 3: Chạy test riêng lẻ**

```powershell
# Chạy đơn test cụ thể
cd SeleniumTests
dotnet test --filter "TestSearchWithValidKeyword"

# Chạy test theo class
dotnet test --filter "VnExpressSearchTests"

# Chạy theo category
dotnet test --filter "Category=Logic"
```

---

## 📊 KỲ VỌNG KẾT QUẢ

### **Unit Tests (NUnit)**
```
✅ Passed:  19/19
❌ Failed:  0
⏱️  Thời gian: < 1 giây

Ghi chú: LUÔN PASS (không phụ internet)
```

### **UI Tests (Selenium)**
```
✅ Passed:  14/16 (87.5%)
❌ Failed:  2 (do website chậm)
⏱️  Thời gian: 2-3 phút

Ghi chú: Có thể fail nếu website slowdown
```

### **Performance Tests (JMeter)**
```
✅ 500 users: Response time < 5 giây
✅ 1000 users: Website chịu được
⏱️  Thời gian: ~10 phút

Ghi chú: Xem chi tiết ở report
```

---

## 🔍 SỰ KHÁC BIỆT GIỮA 3 LOẠI TEST

| Tiêu chí | UI Tests | Unit Tests | Performance Tests |
|---------|----------|-----------|-----------------|
| **Mục đích** | Test giao diện | Test logic | Test tải |
| **Cần browser?** | Có ✅ | Không ❌ | Không ❌ |
| **Tốc độ** | Chậm (2-3 phút) | Nhanh (< 1s) | Chậm (10 phút) |
| **Phụ thuộc website?** | Có (có thể slow) | Không | Có (website phải up) |
| **Số lượng test** | 16 | 19 | 3 scenario |
| **Test gì?** | Click, search, view | Calculate, validate | Concurrent users |

---

## 💡 VÍ DỤ TƯƠNG TỰ NGOÀI ĐỜI

Dự án này giống như:

**Nếu VnExpress là một nhà hàng:**
- 🤖 **UI Tests** = Nhân viên kiểm tra: Khách có được phục vụ tốt không?
- 🧪 **Unit Tests** = Kiểm tra công thức nấu ăn: Hương vị có đúng không?
- ⚡ **Performance Tests** = Kiểm tra: Nhà hàng chịu được bao nhiêu khách cùng lúc?

---

## 📋 QUYỂN TRÌNH TỰ ĐỘNG

```
Lập trình viên viết code mới
          ↓
Commit code
          ↓
🤖 Tự động chạy tất cả tests
          ├→ Unit Tests (kiểm tra logic)
          ├→ UI Tests (kiểm tra giao diện)
          └→ Performance Tests (kiểm tra tải)
          ↓
Tất cả pass? ✅
          ├→ YES: Deploy lên production ✅
          └→ NO: Báo lỗi, sửa code 🔧
          ↓
Website hoạt động tốt 🎉
```

---

## 🎓 KIẾN THỨC DÙNG TRONG DỰ ÁN

### **Selenium WebDriver**
- Page Object Model (POM)
- Explicit/Implicit waits
- WebDriver management
- Browser automation

### **C# & .NET**
- NUnit framework
- Assert statements
- Test attributes ([Test], [Setup], [TearDown])
- Async programming

### **QA Concepts**
- Test case design
- Test coverage
- Bug reporting
- Test metrics

---

## 🔗 THAM KHẢO

- **Selenium**: https://www.selenium.dev/
- **NUnit**: https://nunit.org/
- **JMeter**: https://jmeter.apache.org/
- **VnExpress**: https://vnexpress.net/
- **Zing News**: https://zingnews.vn/

---

## ❓ THƯỜNG GẶP

**Q: Tại sao UI tests fail?**  
A: Thường do website chậm, không phải lỗi code. Unit tests pass = code OK.

**Q: Có thể chạy tests trên CI/CD không?**  
A: Có, thay `dotnet test` bằng `dotnet test --logger trx` trên GitHub Actions, Jenkins, etc.

**Q: Làm sao thêm test mới?**  
A: Thêm file `.cs` trong Tests folder, theo format hiện tại.

**Q: Tại sao cần 3 loại test?**  
A: 
- Unit tests = test nhanh, reliable 
- UI tests = test realistic (user perspective)
- Performance = test chịu tải thực tế

---

**Tác giả:** Dự án kiểm thử QA  
**Ngày:** April 2026  
**Status:** ✅ Hoàn thành
