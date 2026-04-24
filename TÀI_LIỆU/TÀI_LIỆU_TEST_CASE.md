# TÀI LIỆU TEST CASE
## Dự Án Kiểm Thử Website Tin Tức

**Tên Dự Án:** Kiểm Thử Website (VnExpress & Zing News)  
**Kỹ Sư Kiểm Thử:** [Tên Bạn]  
**Ngày:** 16 Tháng 4, 2026  
**Phiên Bản:** 1.0

---

## 1. TEST CASE - CHỨC NĂNG TÌM KIẾM

### TC001_VNE: Tìm Kiếm với Từ Khóa Hợp Lệ
- **ID:** TC001_VNE
- **Danh Mục:** Giao Diện - Tìm Kiếm
- **Loại Kiểm Thử:** Kiểm Thử Chức Năng
- **Mức Độ Ưu Tiên:** Cao
- **Điều Kiện Tiên Quyết:** Trang Chủ VnExpress đã tải
- **Các Bước:**
  1. Truy cập https://vnexpress.net/
  2. Nhập từ khóa "Vietnam" vào ô tìm kiếm
  3. Nhấp nút Tìm Kiếm
- **Kết Quả Dự Kiến:** Hiển thị danh sách bài viết chứa "Vietnam"
- **Kết Quả Thực Tế:** [Sẽ được điền sau khi thực hiện]
- **Tình Trạng:** [PASS/FAIL]

### TC002_VNE: Tìm Kiếm với Từ Khóa Trống
- **ID:** TC002_VNE
- **Danh Mục:** Giao Diện - Tìm Kiếm
- **Loại Kiểm Thử:** Kiểm Thử Phủ Định
- **Mức Độ Ưu Tiên:** Cao
- **Điều Kiện Tiên Quyết:** Trang Chủ VnExpress đã tải
- **Các Bước:**
  1. Truy cập https://vnexpress.net/
  2. Để ô tìm kiếm trống
  3. Nhấp nút Tìm Kiếm
- **Kết Quả Dự Kiến:** Hiển thị tin nhắn lỗi hoặc không có kết quả
- **Kết Quả Thực Tế:** [Sẽ được điền sau khi thực hiện]
- **Tình Trạng:** [PASS/FAIL]

### TC003_VNE: Tìm Kiếm với Từ Khóa Dài (>100 ký tự)
- **ID:** TC003_VNE
- **Danh Mục:** Giao Diện - Tìm Kiếm
- **Loại Kiểm Thử:** Kiểm Thử Giới Hạn
- **Mức Độ Ưu Tiên:** Trung Bình
- **Điều Kiện Tiên Quyết:** Trang Chủ VnExpress đã tải
- **Các Bước:**
  1. Truy cập https://vnexpress.net/
  2. Nhập từ khóa với 150+ ký tự
  3. Nhấp nút Tìm Kiếm
- **Kết Quả Dự Kiến:** Hệ thống xử lý một cách nhẹ nhàng hoặc hiển thị lỗi
- **Kết Quả Thực Tế:** [Sẽ được điền sau khi thực hiện]
- **Tình Trạng:** [PASS/FAIL]

### TC004_VNE: Tìm Kiếm với Ký Tự Đặc Biệt
- **ID:** TC004_VNE
- **Danh Mục:** Giao Diện - Tìm Kiếm
- **Loại Kiểm Thử:** Kiểm Thử Phủ Định
- **Mức Độ Ưu Tiên:** Trung Bình
- **Điều Kiện Tiên Quyết:** Trang Chủ VnExpress đã tải
- **Steps:**
  1. Navigate to https://vnexpress.net/
  2. Enter special characters "!@#$%" in search box
  3. Click Search button
- **Expected Result:** Show error or no results
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC005_VNE: Verify Search Results Contain Keyword
- **ID:** TC005_VNE
- **Category:** UI - Search
- **Test Type:** Validation Testing
- **Priority:** High
- **Precondition:** Search results are displayed
- **Steps:**
  1. Navigate to https://vnexpress.net/
  2. Search for "Kinh tế"
  3. Verify all results contain the keyword
- **Expected Result:** All results contain search keyword
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC006_VNE: Click First Article in Search Results
- **ID:** TC006_VNE
- **Category:** UI - Search & Navigation
- **Test Type:** Functional Testing
- **Priority:** High
- **Precondition:** Search results are displayed with articles
- **Steps:**
  1. Search for "News"
  2. Click on first article in results
- **Expected Result:** Article detail page loads with title and content
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

---

## 2. TEST CASE - FILTER/CATEGORY FUNCTIONALITY

### TC007_VNE: Category List is Displayed
- **ID:** TC007_VNE
- **Category:** UI - Category Filter
- **Test Type:** Functional Testing
- **Priority:** High
- **Precondition:** VnExpress homepage is loaded
- **Steps:**
  1. Navigate to https://vnexpress.net/
  2. Check if category menu is visible
- **Expected Result:** Display list of categories (Thế giới, Kinh tế, etc.)
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC008_VNE: Select Category from Menu
- **ID:** TC008_VNE
- **Category:** UI - Category Filter
- **Test Type:** Functional Testing
- **Priority:** High
- **Precondition:** Category menu is visible
- **Steps:**
  1. Navigate to https://vnexpress.net/
  2. Click on a category (e.g., "Thế giới")
  3. Verify page navigation
- **Expected Result:** Navigate to category page and display related articles
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC009_VNE: Category URL Changes
- **ID:** TC009_VNE
- **Category:** UI - Category Filter
- **Test Type:** Validation Testing
- **Priority:** Medium
- **Precondition:** Homepage is loaded
- **Steps:**
  1. Record initial URL
  2. Select a category
  3. Check if URL changed
- **Expected Result:** URL changes to reflect selected category
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

---

## 3. TEST CASE - ARTICLE DETAIL FUNCTIONALITY

### TC010_VNE: View Article Details
- **ID:** TC010_VNE
- **Category:** UI - Article Detail
- **Test Type:** Functional Testing
- **Priority:** High
- **Precondition:** Search results are displayed
- **Steps:**
  1. Search for "Công nghệ"
  2. Click first article
- **Expected Result:** Display article title, content, and publication date
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC011_VNE: Article Title is Not Empty
- **ID:** TC011_VNE
- **Category:** UI - Article Detail
- **Test Type:** Validation Testing
- **Priority:** High
- **Precondition:** Article detail page is loaded
- **Steps:**
  1. Open article detail page
  2. Check article title exists
- **Expected Result:** Article title is displayed and not empty
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC012_VNE: Article Content is Not Empty
- **ID:** TC012_VNE
- **Category:** UI - Article Detail
- **Test Type:** Validation Testing
- **Priority:** High
- **Precondition:** Article detail page is loaded
- **Steps:**
  1. Open article detail page
  2. Check article content exists
- **Expected Result:** Article content is displayed and not empty
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

---

## 4. TEST CASE - LOGIC/API TESTING (NUnit)

### TC101: Validate Empty Keyword
- **ID:** TC101
- **Category:** Logic - Search Validation
- **Test Type:** Unit Testing
- **Priority:** High
- **Input:** keyword = ""
- **Expected Result:** Return false (invalid)
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC102: Validate Long Keyword (>200 characters)
- **ID:** TC102
- **Category:** Logic - Search Validation
- **Test Type:** Unit Testing
- **Priority:** High
- **Input:** keyword with 250 characters
- **Expected Result:** Return false (invalid)
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC103: Validate Special Characters Only
- **ID:** TC103
- **Category:** Logic - Search Validation
- **Test Type:** Unit Testing
- **Priority:** Medium
- **Input:** keyword = "!@#$%"
- **Expected Result:** Return false (no alphanumeric)
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC104: Sanitize SQL Injection
- **ID:** TC104
- **Category:** Logic - Security
- **Test Type:** Unit Testing
- **Priority:** Critical
- **Input:** keyword = "'; DROP TABLE users;--"
- **Expected Result:** Remove SQL injection patterns
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

### TC105: Search Results Return Valid Data
- **ID:** TC105
- **Category:** Logic - Search Service
- **Test Type:** Unit Testing
- **Priority:** High
- **Input:** keyword = "Vietnam"
- **Expected Result:** Return list with valid article objects
- **Actual Result:** [To be filled after execution]
- **Status:** [PASS/FAIL]

---

## 5. TEST CASE - PERFORMANCE TESTING (JMeter)

### TC201: VnExpress Homepage - 500 Users
- **ID:** TC201
- **Category:** Performance - Load Testing
- **Test Type:** Load Testing
- **Priority:** High
- **Scenario:** 500 concurrent users accessing homepage
- **Duration:** 300 seconds (5 minutes)
- **Expected Result:**
  - Average Response Time < 2 seconds
  - Error Rate < 1%
  - Throughput > 50 requests/second
- **Actual Result:** [To be filled after test execution]
- **Status:** [PASS/FAIL]

### TC202: Zing News Homepage - 500 Users
- **ID:** TC202
- **Category:** Performance - Load Testing
- **Test Type:** Load Testing
- **Priority:** High
- **Scenario:** 500 concurrent users accessing homepage
- **Duration:** 300 seconds (5 minutes)
- **Expected Result:**
  - Average Response Time < 2 seconds
  - Error Rate < 1%
  - Throughput > 50 requests/second
- **Actual Result:** [To be filled after test execution]
- **Status:** [PASS/FAIL]

### TC203: VnExpress Stress Test - 1000 Users
- **ID:** TC203
- **Category:** Performance - Stress Testing
- **Test Type:** Stress Testing
- **Priority:** High
- **Scenario:** 1000 concurrent users
- **Duration:** 300 seconds
- **Expected Result:** System remains stable, identify breaking point
- **Actual Result:** [To be filled after test execution]
- **Status:** [PASS/FAIL]

---

## 6. TEST SUMMARY

| Category | Total TC | Passed | Failed | Blocked |
|----------|----------|--------|--------|---------|
| UI Search | 6 | - | - | - |
| Category Filter | 3 | - | - | - |
| Article Detail | 3 | - | - | - |
| Logic/API | 5 | - | - | - |
| Performance | 3 | - | - | - |
| **TOTAL** | **20** | **-** | **-** | **-** |

---

**Prepared by:** [Your Name]  
**Reviewed by:** [Reviewer Name]  
**Approval Date:** [Date]
