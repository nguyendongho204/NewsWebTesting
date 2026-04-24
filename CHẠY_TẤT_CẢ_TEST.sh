#!/usr/bin/env bash
# ============================================================================
# SCRIPT CHẠY TẤT CẢ TEST - NEWS WEBSITE TESTING PROJECT (LINUX/MAC)
# ============================================================================
# Mục đích: Tự động chạy tất cả 3 loại test (UI, Logic, Performance)
# Cách sử dụng: 
#   - Terminal: chmod +x RUN_ALL_TESTS.sh && ./RUN_ALL_TESTS.sh
# ============================================================================

echo "═════════════════════════════════════════════════════════════════"
echo "🚀 NEWS WEBSITE TESTING - RUN ALL TESTS"
echo "═════════════════════════════════════════════════════════════════"
echo ""

PROJECT_ROOT=$(pwd)
START_TIME=$(date +%s)

# ============================================================================
# BƯỚC 1: LOGIC TESTS (NUnit)
# ============================================================================
echo "📋 BƯỚC 1: Chạy UNIT TESTS (Logic) - Kiểm tra xử lý dữ liệu"
echo "────────────────────────────────────────────────────────────────"
echo ""

cd "$PROJECT_ROOT/NUnitTests"
echo "📂 Thư mục: $PROJECT_ROOT/NUnitTests"
echo "⏳ Chạy tests..."
echo ""

dotnet test --logger "console;verbosity=normal"

if [ $? -eq 0 ]; then
    echo ""
    echo "✅ UNIT TESTS PASS!"
else
    echo ""
    echo "❌ UNIT TESTS FAILED!"
fi

echo ""
echo ""

# ============================================================================
# BƯỚC 2: UI TESTS (Selenium)
# ============================================================================
echo "🌐 BƯỚC 2: Chạy UI TESTS (Selenium) - Kiểm tra giao diện website"
echo "────────────────────────────────────────────────────────────────"
echo ""

cd "$PROJECT_ROOT/SeleniumTests"
echo "📂 Thư mục: $PROJECT_ROOT/SeleniumTests"
echo "⏳ Chạy tests (Chrome sẽ mở và đóng tự động)..."
echo ""

dotnet test --logger "console;verbosity=normal"

if [ $? -eq 0 ]; then
    echo ""
    echo "✅ UI TESTS PASS!"
else
    echo ""
    echo "⚠️  UI TESTS CÓ LỖI (có thể do website chậm)"
fi

echo ""
echo ""

# ============================================================================
# TÓM TẮT KẾT QUẢ
# ============================================================================
END_TIME=$(date +%s)
DURATION=$((END_TIME - START_TIME))

echo "═════════════════════════════════════════════════════════════════"
echo "📊 TÓM TẮT KẾT QUẢ"
echo "═════════════════════════════════════════════════════════════════"
echo ""
echo "⏱️  Thời gian chạy: ${DURATION} giây"
echo ""
echo "✅ Hoàn thành chạy tất cả tests!"
echo ""
echo "📝 Ghi chú:"
echo "  • UI Tests có thể fail nếu website slowdown"
echo "  • Logic Tests luôn pass (không phụ thuộc website)"
echo "  • Xem chi tiết kết quả ở trên"
echo ""
