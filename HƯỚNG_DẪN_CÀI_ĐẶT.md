# HƯỚNG DẪN CÀI ĐẶT DỰ ÁN

## Yêu Cầu Hệ Thống

### Phần Cứng
- Máy tính với tối thiểu **4GB RAM**
- Ổ đĩa: **5GB không gian trống**
- Kết nối Internet để tải dependencies

### Phần Mềm
- **Windows 10+**, macOS 10.15+, hoặc Linux (Ubuntu 18+)
- **.NET 6.0 SDK** (tải từ https://dotnet.microsoft.com/download)
- **Visual Studio Code** hoặc **Visual Studio 2022**
- **Google Chrome** (phiên bản mới nhất)
- **Git** (tùy chọn, để clone dự án)

---

## BƯỚC 1: Cài Đặt .NET SDK

### Windows
1. Tải .NET 6.0 SDK từ https://dotnet.microsoft.com/download
2. Chạy installer và làm theo các bước
3. Kiểm tra cài đặt:
   ```powershell
   dotnet --version
   ```

### macOS / Linux
```bash
# macOS (dùng Homebrew)
brew install dotnet-sdk

# Linux (Ubuntu/Debian)
sudo apt-get install dotnet-sdk-6.0
```

---

## BƯỚC 2: Cài Đặt Visual Studio Code

1. Tải từ https://code.visualstudio.com/
2. Cài đặt extension cho C#:
   - Mở VS Code
   - Đi tới Extensions (Ctrl+Shift+X)
   - Tìm "C#" và cài đặt

---

## BƯỚC 3: Cài Đặt Chrome & JMeter (Tùy Chọn)

### Chrome
- Tải từ https://www.google.com/chrome/
- Cài đặt để chạy UI tests với Selenium

### JMeter (cho Performance Testing)
```bash
# Windows (dùng Chocolatey)
choco install jmeter

# macOS
brew install jmeter

# Linux
sudo apt-get install jmeter
```

---

## BƯỚC 4: Tải Project

### Cách 1: Với Git
```bash
git clone <repository-url>
cd NewsWebTesting
```

### Cách 2: Tải ZIP
1. Tải file từ repository
2. Giải nén vào thư mục
3. Mở terminal/PowerShell vào thư mục đó

---

## BƯỚC 5: Cài Đặt Dependencies

```bash
# Chạy từ thư mục gốc
cd d:\Nam4\qlkt\NewsWebTesting

# Cài đặt cho Kiểm thử Giao diện
cd TEST_GIAO_DIỆN
dotnet restore

# Cài đặt cho Kiểm thử Logic
cd ../TEST_LOGIC
dotnet restore

# Quay lại thư mục gốc
cd ..
```

---

## BƯỚC 6: Kiểm Tra Cài Đặt

```bash
# Kiểm tra .NET
dotnet --version

# Kiểm tra Git (nếu dùng)
git --version

# Chạy test đơn giản
cd TEST_LOGIC
dotnet test --filter "TestValidateEmptyKeyword"
```

✅ Nếu không có lỗi, cài đặt thành công!

---

## Xử Lý Sự Cố

### Lỗi: ".NET not found"
**Giải pháp:** Cài đặt lại .NET 6.0 SDK

### Lỗi: "Chrome version mismatch"
**Giải pháp:** Chạy cài đặt WebDriverManager
```bash
cd TEST_GIAO_DIỆN
dotnet add package WebDriverManager --version 2.17.0
```

### Lỗi: "No tests found"
**Giải pháp:** Đảm bảo bạn đang ở thư mục đúng
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

**Xong! Giờ bạn có thể chạy test bằng cách:** `.\CHẠY_TẤT_CẢ_TEST.ps1`
