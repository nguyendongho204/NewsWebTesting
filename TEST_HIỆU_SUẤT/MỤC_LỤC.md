# Hướng Dẫn Kiểm Thử Hiệu Suất JMeter

## Cấu Hình Kiểm Thử

### Các Kịch Bản:
1. **Trang Chủ VnExpress - 500 Người Dùng**
   - Thời gian ramp-up: 60 giây
   - Thời lượng: 300 giây (5 phút)
   - Yêu cầu trên mỗi người dùng: 2 (Trang chủ + Tìm kiếm)

2. **Trang Chủ Zing News - 500 Người Dùng**
   - Thời gian ramp-up: 60 giây
   - Thời lượng: 300 giây (5 phút)
   - Yêu cầu trên mỗi người dùng: 1 (Trang chủ)

### Hồ Sơ Tải:
- **Tải Nhẹ**: 100 người dùng
- **Tải Vừa**: 500 người dùng
- **Tải Nặng**: 1000 người dùng

### Các Chỉ Số Hiệu Suất Cần Theo Dõi:
1. **Thời Gian Phản Hồi**: Trung bình, Tối thiểu, Tối đa, Phân vị thứ 95
2. **Thông Lượng**: Yêu cầu trên mỗi giây
3. **Tỷ Lệ Lỗi**: Phần trăm yêu cầu thất bại
4. **CPU & Bộ Nhớ**: Sử dụng tài nguyên máy chủ

### Tiêu Chí Chấp Nhận:
- Thời gian phản hồi trung bình < 2 giây
- Thời gian phản hồi phân vị thứ 95 < 5 giây
- Tỷ lệ lỗi < 1%
- Thông lượng > 50 yêu cầu/giây

## Chạy Kiểm Thử:

```bash
# Chế Độ GUI (để tạo/chỉnh sửa kế hoạch kiểm thử):
jmeter -t NewsWebsite_PerformanceTest.jmx

# Chế Độ Dòng Lệnh (để kiểm thử sản xuất):
jmeter -n -t NewsWebsite_PerformanceTest.jmx -l results.jtl -j jmeter.log -e -o results_html

# Với các thuộc tính tùy chỉnh:
jmeter -n -t NewsWebsite_PerformanceTest.jmx -l results.jtl -Jjmeter.save.saveall=true
```

## Giải Thích Kết Quả:

1. **Báo Cáo Tóm Tắt**:
   - Label: Tên yêu cầu
   - Samples: Số lượng yêu cầu
   - Average: Thời gian phản hồi trung bình
   - Min: Thời gian phản hồi tối thiểu
   - Max: Thời gian phản hồi tối đa
   - Std. Dev.: Độ lệch chuẩn
   - Error Rate: Phần trăm yêu cầu thất bại
   - Throughput: Yêu cầu trên mỗi giây
   - Received KB/sec: Dữ liệu nhận được trên mỗi giây
   - Sent KB/sec: Dữ liệu gửi trên mỗi giây

2. **Kết Quả Biểu Đồ**:
   - Biểu Đồ Thời Gian Phản Hồi: Hiển thị hiệu suất theo thời gian
   - Biểu Đồ Thông Lượng: Hiển thị tải theo thời gian

## Khắc Phục Sự Cố:

1. **Tỷ Lệ Lỗi Cao**:
   - Kiểm tra xem trang web có thể truy cập được không
   - Xác minh kết nối mạng
   - Kiểm tra nhật ký máy chủ trang web

2. **Thời Gian Phản Hồi Chậm**:
   - Có thể chỉ ra điểm nghẽn máy chủ
   - Kiểm tra tài nguyên máy chủ (CPU, Bộ nhớ, I/O Đĩa)
   - Cân nhắc cân bằng tải

3. **Hết Thời Gian Kết Nối**:
   - Tăng thời gian chờ kết nối
   - Kiểm tra xem trang web có thể xử lý các kết nối đồng thời không
   - Xác minh các quy tắc tường lửa

## Các Bước Tiếp Theo:

1. Chạy kiểm thử với các hồ sơ tải khác nhau
2. Phân tích kết quả và xác định các điểm nghẽn
3. Điều chỉnh cấu hình máy chủ nếu cần
4. Chạy các kiểm thử áp lực để tìm điểm gãy
5. Ghi lại các phát hiện và khuyến nghị
