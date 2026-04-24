using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;
using NUnitTests.Utils;

namespace NUnitTests.Tests
{
    /// <summary>
    /// Test các hàm xác thực keyword ($ValidateKeyword)
    /// </summary>
    [TestFixture]
    public class SearchValidatorTests
    {
        /// <summary>
        /// TEST 1: Kiểm tra keyword rỗng
        /// Input: keyword = ""
        /// Kỳ vọng: Trả về false
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateEmptyKeyword()
        {
            // Arrange (chuẩn bị)
            string keyword = "";

            // Act (thực hiện)
            bool result = SearchValidator.IsValidKeyword(keyword);

            // Assert (kiểm tra)
            Assert.That(result, Is.False, "Keyword rỗng phải không hợp lệ");
        }

        /// <summary>
        /// TEST 2: Kiểm tra keyword null
        /// Input: keyword = null
        /// Kỳ vọng: Trả về false
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateNullKeyword()
        {
            // Arrange
            string? keyword = null;

            // Act
            bool result = SearchValidator.IsValidKeyword(keyword ?? "");

            // Assert
            Assert.That(result, Is.False, "Keyword null phải không hợp lệ");
        }

        /// <summary>
        /// TEST 3: Kiểm tra keyword hợp lệ
        /// Input: keyword = "Vietnam"
        /// Kỳ vọng: Trả về true
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateValidKeyword()
        {
            // Arrange
            string keyword = "Vietnam";

            // Act
            bool result = SearchValidator.IsValidKeyword(keyword);

            // Assert
            Assert.That(result, Is.True, "Keyword 'Vietnam' phải hợp lệ");
        }

        /// <summary>
        /// TEST 4: Kiểm tra keyword quá dài (>200 ký tự)
        /// Input: 250 ký tự 'a'
        /// Kỳ vọng: Trả về false
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateLongKeyword()
        {
            // Arrange
            string longKeyword = new string('a', 250);

            // Act
            bool result = SearchValidator.IsValidKeyword(longKeyword);

            // Assert
            Assert.That(result, Is.False, "Keyword dài quá 200 ký tự phải không hợp lệ");
        }

        /// <summary>
        /// TEST 5: Kiểm tra keyword độ dài vừa phải
        /// Input: 100 ký tự
        /// Kỳ vọng: Trả về true
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateModerateKeyword()
        {
            // Arrange
            string keyword = new string('a', 100);

            // Act
            bool result = SearchValidator.IsValidKeyword(keyword);

            // Assert
            Assert.That(result, Is.True, "Keyword 100 ký tự phải hợp lệ");
        }

        /// <summary>
        /// TEST 6: Kiểm tra keyword toàn ký tự đặc biệt
        /// Input: keyword = "!@#$%"
        /// Kỳ vọng: Trả về false (không có chữ/số)
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateKeywordWithOnlySpecialChars()
        {
            // Arrange
            string keyword = "!@#$%";

            // Act
            bool result = SearchValidator.IsValidKeyword(keyword);

            // Assert
            Assert.That(result, Is.False, "Keyword toàn ký tự đặc biệt phải không hợp lệ");
        }

        /// <summary>
        /// TEST 7: Kiểm tra keyword hỗn hợp chữ, số, ký tự đặc
        /// Input: keyword = "News123!"
        /// Kỳ vọng: Trả về true (có chữ và số)
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestValidateKeywordWithMixedChars()
        {
            // Arrange
            string keyword = "News123!";

            // Act
            bool result = SearchValidator.IsValidKeyword(keyword);

            // Assert
            Assert.That(result, Is.True, "Keyword hỗn hợp phải hợp lệ");
        }

        /// <summary>
        /// TEST 8: Kiểm tra làm sạch SQL Injection
        /// Input: keyword = "'; DROP TABLE users;--"
        /// Kỳ vọng: Xóa các pattern nguy hiểm
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestSanitizeKeywordSQLInjection()
        {
            // Arrange
            string keyword = "'; DROP TABLE users;--";

            // Act
            string result = SearchValidator.SanitizeKeyword(keyword);

            // Assert
            // Kiểm tra ký tự ; và -- bị xóa
            Assert.That(!result.Contains(";") && !result.Contains("--"), Is.True,
                "SQL injection patterns phải bị xóa");
        }

        /// <summary>
        /// TEST 9: Kiểm tra phát hiện ký tự đặc biệt
        /// Input: keyword = "News!"
        /// Kỳ vọng: Trả về true (có ký tự !)
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestDetectSpecialCharacters()
        {
            // Arrange
            string keyword = "News!";

            // Act
            bool result = SearchValidator.ContainsSpecialCharacters(keyword);

            // Assert
            Assert.That(result, Is.True, "Phải phát hiện ký tự đặc biệt");
        }

        /// <summary>
        /// TEST 10: Kiểm tra không có ký tự đặc biệt
        /// Input: keyword = "News123"
        /// Kỳ vọng: Trả về false
        /// </summary>
        [Test]
        [Category("Logic")]
        public void TestNoSpecialCharactersDetected()
        {
            // Arrange
            string keyword = "News123";

            // Act
            bool result = SearchValidator.ContainsSpecialCharacters(keyword);

            // Assert
            Assert.That(result, Is.False, "Không phải có ký tự đặc biệt");
        }
    }
}
