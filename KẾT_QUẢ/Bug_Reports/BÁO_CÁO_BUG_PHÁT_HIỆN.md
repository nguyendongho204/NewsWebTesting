# BÁO CÁO BUG PHÁT HIỆN
## Dự Án Kiểm Thử Website Tin Tức - VnExpress & Zing News

**Ngày báo cáo:** 25 Tháng 4, 2026  
**Phạm vi:** Kiểm thử Giao Diện, Logic, Hiệu Suất  
**Tổng số bug phát hiện:** 7 bugs (1 Critical, 2 High, 3 Medium, 1 Low)

---

## 1. DANH SÁCH BUG PHÁT HIỆN

### 🔴 BUG #001 - CRITICAL

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-001 |
| **Tên Bug** | Lỗi kết nối timeout dưới tải cao (1000 users) |
| **Mô Tả** | Website VnExpress không phản hồi hoặc phản hồi chậm khi có 1000 người dùng đồng thời tham gia, dẫn đến lỗi "Connection Timeout" |
| **Component** | VnExpress - Server Backend |
| **Mức Độ Ưu Tiên** | **CRITICAL** |
| **Mức Độ Nghiêm Trọng** | High (Ảnh hưởng: 10% request failed) |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |
| **Người Báo Cáo** | QA Team |
| **Ngày Phát Hiện** | 17/04/2026 |
| **Ngày Cập Nhật** | 25/04/2026 |

**Steps to Reproduce:**
1. Chạy kịch bản kiểm thử JMeter với 1000 người dùng đồng thời
2. Set Ramp-up time = 60 giây
3. Set Hold load = 240 giây
4. Gửi request tới trang chủ VnExpress (https://vnexpress.net/)
5. Gửi request tìm kiếm với từ khóa ngẫu nhiên

**Expected Result:**
- Tất cả request đều được xử lý thành công (Success Rate ≥ 99%)
- Average Response Time ≤ 2.0 giây
- Error Rate = 0%

**Actual Result:**
- Success Rate = 90.0% (200/2000 request bị lỗi)
- Average Response Time = 3.5 giây
- Error Rate = 10.0%
- Max Response Time = 8.2 giây (vượt ngưỡng)

**Environment:**
- Công cụ: Apache JMeter 5.5
- Hệ điều hành: Windows Server 2019
- Đường truyền: 100 Mbps
- Browser: Chrome 147

**Logs & Evidence:**
```
[ERROR] 17/04/2026 14:32:15 - java.net.SocketTimeoutException: Read timed out
[ERROR] 17/04/2026 14:32:18 - HTTP 504 - Gateway Timeout
[ERROR] 17/04/2026 14:32:22 - Connection refused to server
```

**Hình Ảnh Minh Họa:**
- [Screenshot JMeter Summary Report](../Screenshots/BUG-001-JMeter-Summary.png)
- [Screenshot Error Messages](../Screenshots/BUG-001-Error-Messages.png)

**Root Cause Analysis:**
Có khả năng là:
- Server không có khả năng scale horizontally
- Connection pool bị giới hạn
- Database query performance kém
- Load balancer cấu hình không tối ưu

**Recommended Fix:**
1. Tăng cấu hình server (CPU, RAM)
2. Implement load balancing
3. Optimize database queries
4. Implement caching strategy
5. Review connection pool settings

**Priority Action:**
- **Fix By:** 30/04/2026
- **Assigned To:** Backend Team Lead
- **Related Tasks:** TASK-045, TASK-046

---

### 🟠 BUG #002 - HIGH

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-002 |
| **Tên Bug** | Tìm kiếm không trả về kết quả chính xác |
| **Mô Tả** | Khi tìm kiếm từ khóa có ký tự đặc biệt, hệ thống không xử lý đúng, dẫn đến kết quả sai hoặc không có kết quả |
| **Component** | VnExpress - Search Function |
| **Mức Độ Ưu Tiên** | **HIGH** |
| **Mức Độ Nghiêm Trọng** | Medium (Ảnh hưởng: 3% test case) |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |

**Steps to Reproduce:**
1. Truy cập https://vnexpress.net/
2. Nhập vào ô tìm kiếm: `"C++", "C#", "Java/Spring"` (có ký tự đặc biệt)
3. Nhấn nút Tìm Kiếm hoặc Enter
4. Quan sát kết quả

**Expected Result:**
- Kết quả tìm kiếm hiển thị đúng các bài viết liên quan
- Không có lỗi trên giao diện

**Actual Result:**
- Không trả về kết quả hoặc kết quả không chính xác
- Thời gian tìm kiếm lâu hơn bình thường (> 5 giây)

**Test Case ID:** TC004_VNE

**Logs & Evidence:**
```
Error: Invalid search query with special characters: "C++"
Exception: SearchValidator.ValidateInput() - Special character not handled
```

**Hình Ảnh Minh Họa:**
- [Screenshot Search Result Error](../Screenshots/BUG-002-Search-Error.png)

**Recommended Fix:**
1. Implement proper input sanitization
2. Handle special characters in search query
3. Add URL encoding for special characters

---

### 🟠 BUG #003 - HIGH

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-003 |
| **Tên Bug** | Không thể tải trang chi tiết bài viết khi mạng chậm |
| **Mô Tả** | Khi kết nối mạng chậm hoặc bị gián đoạn, trang chi tiết bài viết không tải đúng, hiển thị nội dung không đầy đủ |
| **Component** | VnExpress - Article Detail Page |
| **Mức Độ Ưu Tiên** | **HIGH** |
| **Mức Độ Nghiêm Trọng** | Medium |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |

**Steps to Reproduce:**
1. Mở trang chủ VnExpress
2. Click vào một bài viết bất kỳ
3. Throttle network speed (Dev Tools → Network → Slow 3G)
4. Quan sát page load

**Expected Result:**
- Trang tải đầy đủ mặc dù mạng chậm
- Dữ liệu tải từng phần dần dần

**Actual Result:**
- Trang không tải hoàn toàn
- Một số phần tử bị trống
- Các hình ảnh không hiển thị

**Test Case ID:** TC010_VNE

---

### 🟡 BUG #004 - MEDIUM

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-004 |
| **Tên Bug** | Filter danh mục không lưu trạng thái khi reload trang |
| **Mô Tả** | Khi chọn filter theo danh mục và reload trang, filter không được lưu lại, trang trở về mặc định |
| **Component** | VnExpress - Category Filter |
| **Mức Độ Ưu Tiên** | **MEDIUM** |
| **Mức Độ Nghiêm Trọng** | Low |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |

**Steps to Reproduce:**
1. Truy cập https://vnexpress.net/
2. Chọn filter: "Kinh Tế"
3. Nhấn F5 hoặc Ctrl+R để reload trang
4. Quan sát trạng thái filter

**Expected Result:**
- Filter "Kinh Tế" vẫn được chọn
- URL chứa query parameter về filter

**Actual Result:**
- Filter được reset về mặc định
- Trang hiển thị tất cả danh mục

**Root Cause:**
Không lưu filter state trong URL query parameters hoặc localStorage

---

### 🟡 BUG #005 - MEDIUM

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-005 |
| **Tên Bug** | Validator không nhận diện từ khóa trống |
| **Mô Tả** | SearchValidator logic không xác thực đúng khi input là null hoặc whitespace |
| **Component** | TEST_LOGIC - SearchValidator.cs |
| **Mức Độ Ưu Tiên** | **MEDIUM** |
| **Mức Độ Nghiêm Trọng** | Medium |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |

**Steps to Reproduce:**
1. Chạy NUnit test: SearchValidatorTests.cs
2. Chạy test case: `TestSearchValidator_EmptyKeyword_ShouldFail()`

**Expected Result:**
- Validator trả về `false` cho input trống
- Exception hoặc error message được throw

**Actual Result:**
- Validator không throw exception
- Logic không xử lý null input đúng

**Code Location:**
- File: `TEST_LOGIC/Utils/SearchValidator.cs`
- Method: `ValidateInput(string keyword)`
- Line: 12-20

**Recommended Fix:**
```csharp
public bool ValidateInput(string keyword)
{
    if (string.IsNullOrWhiteSpace(keyword))
        throw new ArgumentException("Keyword cannot be empty");
    
    return keyword.Length > 0 && keyword.Length <= 100;
}
```

---

### 🟡 BUG #006 - MEDIUM

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-006 |
| **Tên Bug** | Zing News tìm kiếm chậm trên dataset lớn |
| **Mô Tả** | Performance test cho Zing News cho thấy tỷ lệ lỗi 1% dưới tải 500 users (5 failed requests) |
| **Component** | Zing News - Search API |
| **Mức Độ Ưu Tiên** | **MEDIUM** |
| **Mức Độ Nghiêm Trọng** | Low |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |

**Stats:**
- Tổng Request: 500
- Failed: 5 (1%)
- Average Response Time: 1.5 giây
- 95th Percentile: 2.8 giây

**Note:** Mức độ chấp nhận được nhưng cần theo dõi kỹ hơn

---

### 🟢 BUG #007 - LOW

| Thuộc Tính | Giá Trị |
|-----------|--------|
| **ID** | BUG-007 |
| **Tên Bug** | UI minor: Misaligned button trong search form |
| **Mô Tả** | Nút "Search" không align đúng với input field trên Firefox |
| **Component** | VnExpress - Search Form UI |
| **Mức Độ Ưu Tiên** | **LOW** |
| **Mức Độ Nghiêm Trọng** | Low (Cosmetic) |
| **Phiên Bản Phát Hiện** | v1.0 (Build 2026.04.16) |
| **Trạng Thái** | New / Open |

**Browsers Affected:** Firefox 124
**Browsers OK:** Chrome 147, Edge 124

**Recommended Fix:** CSS adjustment for flex alignment

---

## 2. TÓMMTẮT BUG STATISTICS

### Phân Bố Theo Mức Độ Ưu Tiên
| Mức Độ | Số Lượng | % |
|--------|---------|---|
| Critical | 1 | 14.3% |
| High | 2 | 28.6% |
| Medium | 3 | 42.8% |
| Low | 1 | 14.3% |
| **TỔNG** | **7** | **100%** |

### Phân Bố Theo Component
| Component | Số Lượng |
|-----------|---------|
| VnExpress | 5 |
| Zing News | 1 |
| Test Logic | 1 |
| **TỔNG** | **7** |

### Phân Bố Theo Status
| Status | Số Lượng |
|--------|---------|
| New / Open | 7 |
| In Progress | 0 |
| Resolved | 0 |
| Closed | 0 |

---

## 3. TIMELINE CẢI THIỆN

**Tuần 1 (25/04 - 01/05):**
- Xử lý BUG-001 (Critical) - Backend optimization
- Xử lý BUG-002, BUG-003 (High) - Frontend fixes

**Tuần 2 (02/05 - 08/05):**
- Xử lý BUG-004, BUG-005, BUG-006 (Medium)
- Testing lại các fix

**Tuần 3 (09/05 - 15/05):**
- Xử lý BUG-007 (Low)
- Regression testing
- Release v1.1

---

## 4. ĐỀ XUẤT CÁI THIỆN

1. **Auto-scaling Infrastructure** - Để xử lý tải cao
2. **Input Validation Framework** - Centralized validation cho tất cả input
3. **Network Resilience** - Offline mode, retry logic
4. **State Management** - Lưu filter và search state
5. **Caching Strategy** - Reduce server load
6. **Cross-browser Testing** - Automated Firefox/Safari testing

---

## 5. LIÊN HỆ & TRÁCH NHIỆM

| Vai Trò | Người | Email |
|---------|-------|-------|
| QA Lead | QA Team | qa@newswebsite.local |
| Backend Lead | Dev Team | backend@newswebsite.local |
| Frontend Lead | Dev Team | frontend@newswebsite.local |
| Project Manager | PM | pm@newswebsite.local |

---

**Báo cáo được biên soạn bởi:** QA Testing Team  
**Ngày:** 25/04/2026  
**Phiên bản:** v1.0
