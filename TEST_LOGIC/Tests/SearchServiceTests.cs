using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;
using NUnitTests.Utils;

namespace NUnitTests.Tests
{
    /// <summary>
    /// Test dịch vụ tìm kiếm (SearchService)
    /// </summary>
    [TestFixture]
    public class SearchServiceTests
    {
        private SearchService searchService = null!;

        // Chạy trước mỗi test
        [SetUp]
        public void Setup()
        {
            searchService = new SearchService();
        }

        /// <summary>
        /// TEST 1: Tìm kiếm với keyword hợp lệ
        /// Input: keyword = "Vietnam"
        /// Kỳ vọng: Trả về danh sách kết quả
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSearchWithValidKeyword()
        {
            // Arrange
            string keyword = "Vietnam";

            // Act
            var results = searchService.Search(keyword);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Phải có kết quả");
            // Kiểm tra tất cả kết quả đều chứa keyword
            Assert.That(results.All(r => r.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) || 
                                          r.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)), Is.True,
                         "Tất cả kết quả phải chứa keyword");
        }

        /// <summary>
        /// TEST 2: Tìm kiếm với keyword rỗng
        /// Input: keyword = ""
        /// Kỳ vọng: Trả về danh sách rỗng
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSearchWithEmptyKeyword()
        {
            // Arrange
            string keyword = "";

            // Act
            var results = searchService.Search(keyword);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Keyword rỗng không trả về kết quả");
        }

        /// <summary>
        /// TEST 3: Tìm kiếm không có kết quả
        /// Input: keyword = "xyzabc123notexist"
        /// Kỳ vọng: Danh sách rỗng
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSearchWithNoResults()
        {
            // Arrange
            string keyword = "xyzabc123notexist";

            // Act
            var results = searchService.Search(keyword);

            // Assert
            Assert.That(results.Count, Is.EqualTo(0), "Keyword không tồn tại trả về 0 kết quả");
        }

        /// <summary>
        /// TEST 4: Tìm kiếm không phân biệt hoa thường
        /// Input: keyword = "VIETNAM" (chữ hoa)
        /// Kỳ vọng: Trả về kết quả chứa "Vietnam"
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSearchCaseInsensitive()
        {
            // Arrange
            string keyword = "VIETNAM";

            // Act
            var results = searchService.Search(keyword);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Tìm kiếm phải không phân biệt hoa thường");
        }

        /// <summary>
        /// TEST 5: Lấy bài viết theo ID
        /// Input: ID = 1
        /// Kỳ vọng: Trả về bài viết có ID = 1
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestGetResultById()
        {
            // Arrange
            int id = 1;

            // Act
            var result = searchService.GetResultById(id);

            // Assert
            Assert.That(result, Is.Not.Null, "Phải tìm thấy bài viết");
            Assert.That(result.Id, Is.EqualTo(id), "ID phải khớp");
        }

        /// <summary>
        /// TEST 6: Lấy bài viết với ID không tồn tại
        /// Input: ID = 999
        /// Kỳ vọng: Trả về null
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestGetResultByInvalidId()
        {
            // Arrange
            int id = 999;

            // Act
            var result = searchService.GetResultById(id);

            // Assert
            Assert.That(result, Is.Null, "ID không tồn tại trả về null");
        }

        /// <summary>
        /// TEST 7: Lấy bài viết theo chuyên mục
        /// Input: category = "News"
        /// Kỳ vọng: Trả về danh sách bài viết thuộc chuyên mục đó
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestGetResultsByCategory()
        {
            // Arrange
            string category = "News";

            // Act
            var results = searchService.GetResultsByCategory(category);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Phải có bài viết trong chuyên mục");
            // Kiểm tra tất cả bài viết đều thuộc chuyên mục
            Assert.That(results.All(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)), Is.True,
                         "Tất cả bài viết phải thuộc chuyên mục");
        }

        /// <summary>
        /// TEST 8: Kiểm tra thuộc tính bài viết
        /// Input: Lấy bài viết đầu tiên
        /// Kỳ vọng: Tất cả thuộc tính không rỗng
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSearchResultProperties()
        {
            // Arrange
            string keyword = "Vietnam";

            // Act
            var results = searchService.Search(keyword);

            // Assert
            Assert.That(results.Count, Is.GreaterThan(0), "Phải có kết quả");
            var firstResult = results.First();
            
            Assert.That(!string.IsNullOrEmpty(firstResult.Title), Is.True, "Tiêu đề không rỗng");
            Assert.That(!string.IsNullOrEmpty(firstResult.Url), Is.True, "URL không rỗng");
            Assert.That(!string.IsNullOrEmpty(firstResult.Category), Is.True, "Chuyên mục không rỗng");
            Assert.That(firstResult.ViewCount, Is.GreaterThan(0), "Lượt xem > 0");
        }

        /// <summary>
        /// TEST 9: Kiểm tra số lượng kết quả
        /// Input: Search "Vietnam" (nhiều kết quả)
        /// Kỳ vọng: Số lượng kết quả hợp lý
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSearchResultsReasonableSize()
        {
            // Arrange
            string keyword = "Vietnam";

            // Act
            var results = searchService.Search(keyword);

            // Assert
            // Số kết quả không vượt quá 100
            Assert.That(results.Count <= 100, Is.True, "Số kết quả không quá 100");
            // Kiểm tra độ dài tiêu đề
            foreach (var result in results)
            {
                Assert.That(result.Title.Length <= 500, Is.True, "Tiêu đề không quá 500 ký tự");
            }
        }
    }
}
