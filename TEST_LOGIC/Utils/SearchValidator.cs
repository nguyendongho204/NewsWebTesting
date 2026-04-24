using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTests.Utils
{
    /// <summary>
    /// Lớp xác thực tính hợp lệ của keyword tìm kiếm
    /// </summary>
    public class SearchValidator
    {
        // Kiểm tra keyword có hợp lệ không
        // Trả về: true nếu hợp lệ, false nếu không
        public static bool IsValidKeyword(string keyword)
        {
            // Nếu keyword rỗng hoặc toàn khoảng trắng
            if (string.IsNullOrWhiteSpace(keyword))
                return false;

            // Keyword không được dài quá 200 ký tự
            if (keyword.Length > 200)
                return false;

            // Phải có ít nhất 1 ký tự là chữ hoặc số
            return keyword.Any(c => char.IsLetterOrDigit(c));
        }

        // Làm sạch keyword để tránh tấn công SQL injection
        // Ví dụ: "'; DROP TABLE" → "''; DROP TABLE"
        public static string SanitizeKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return string.Empty;

            // Xóa các ký tự nguy hiểm của SQL injection
            keyword = keyword.Replace("'", "''");    // '' để escape dấu '
            keyword = keyword.Replace(";", "");      // Xóa dấu ;
            keyword = keyword.Replace("--", "");     // Xóa comment SQL

            return keyword.Trim();
        }

        // Kiểm tra keyword có ký tự đặc biệt không
        // Trả về: true nếu có, false nếu không
        public static bool ContainsSpecialCharacters(string keyword)
        {
            // Danh sách ký tự đặc biệt cần kiểm tra
            var specialChars = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '=', '+', '[', ']', '{', '}', '|', ';', ':', '"', '<', '>', ',', '.', '?', '/' };
            return keyword.Any(c => specialChars.Contains(c));
        }
    }

    /// <summary>
    /// Lớp đại diện cho một kết quả tìm kiếm
    /// </summary>
    public class SearchResult
    {
        public int Id { get; set; }                    // ID bài viết
        public string Title { get; set; } = string.Empty;       // Tiêu đề
        public string Description { get; set; } = string.Empty; // Mô tả
        public string Url { get; set; } = string.Empty;         // Link bài viết
        public DateTime PublishedDate { get; set; }   // Ngày đăng
        public string Category { get; set; } = string.Empty;    // Chuyên mục
        public int ViewCount { get; set; }             // Số lượt xem
    }

    /// <summary>
    /// Lớp giả lập dịch vụ tìm kiếm (mock service)
    /// </summary>
    public class SearchService
    {
        // Dữ liệu giả lập
        private List<SearchResult> mockResults = new List<SearchResult>
        {
            new SearchResult 
            { 
                Id = 1, 
                Title = "Vietnam News", 
                Description = "Latest news from Vietnam", 
                Url = "https://vnexpress.net/1", 
                PublishedDate = DateTime.Now, 
                Category = "News", 
                ViewCount = 100 
            },
            new SearchResult 
            { 
                Id = 2, 
                Title = "Technology Vietnam", 
                Description = "Tech news", 
                Url = "https://vnexpress.net/2", 
                PublishedDate = DateTime.Now, 
                Category = "Tech", 
                ViewCount = 200 
            },
            new SearchResult 
            { 
                Id = 3, 
                Title = "Sports in Vietnam", 
                Description = "Sports updates", 
                Url = "https://vnexpress.net/3", 
                PublishedDate = DateTime.Now, 
                Category = "Sports", 
                ViewCount = 150 
            }
        };

        // Tìm kiếm bài viết theo keyword
        // Tham số: keyword = từ khoá tìm kiếm
        // Trả về: danh sách bài viết khớp
        public List<SearchResult> Search(string keyword)
        {
            // Kiểm tra keyword có hợp lệ
            if (!SearchValidator.IsValidKeyword(keyword))
                return new List<SearchResult>();

            // Chuyển keyword thành chữ thường để so sánh
            keyword = keyword.ToLower();
            
            // Lọc kết quả: title hoặc description phải chứa keyword
            return mockResults
                .Where(r => r.Title.ToLower().Contains(keyword) || r.Description.ToLower().Contains(keyword))
                .ToList();
        }

        // Lấy bài viết theo ID
        // Tham số: id = ID bài viết
        // Trả về: SearchResult hoặc null nếu không tìm thấy
        public SearchResult? GetResultById(int id)
        {
            return mockResults.FirstOrDefault(r => r.Id == id);
        }

        // Lấy bài viết theo chuyên mục
        // Tham số: category = tên chuyên mục
        // Trả về: danh sách bài viết
        public List<SearchResult> GetResultsByCategory(string category)
        {
            return mockResults.Where(r => r.Category.ToLower() == category.ToLower()).ToList();
        }
    }
}
