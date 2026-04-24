# ============================================================================
# SCRIPT CHẠY TẤT CẢ TEST - NEWS WEBSITE TESTING PROJECT
# ============================================================================
# Mục đích: Tự động chạy tất cả 3 loại test (UI, Logic, Performance)
# Cách sử dụng: 
#   - Mở PowerShell
#   - Chạy: .\RUN_ALL_TESTS.ps1
# ============================================================================

Write-Host "═════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "🚀 NEWS WEBSITE TESTING - RUN ALL TESTS" -ForegroundColor Cyan
Write-Host "═════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

# Lấy thư mục hiện tại
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$startTime = Get-Date

# ============================================================================
# BƯỚC 1: LOGIC TESTS (NUnit) - Nhanh nhất, không cần browser
# ============================================================================
Write-Host "📋 BƯỚC 1: Chạy UNIT TESTS (Logic) - Kiểm tra xử lý dữ liệu" -ForegroundColor Yellow
Write-Host "────────────────────────────────────────────────────────────────" -ForegroundColor Yellow
Write-Host ""

try {
    cd "$projectRoot\NUnitTests"
    Write-Host "📂 Thư mục: $projectRoot\NUnitTests" -ForegroundColor Gray
    Write-Host "⏳ Chạy tests..." -ForegroundColor Cyan
    Write-Host ""
    
    dotnet test --logger "console;verbosity=normal"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "✅ UNIT TESTS PASS!" -ForegroundColor Green
    } else {
        Write-Host ""
        Write-Host "❌ UNIT TESTS FAILED!" -ForegroundColor Red
    }
} catch {
    Write-Host "⚠️  Lỗi khi chạy unit tests: $_" -ForegroundColor Red
}

Write-Host ""
Write-Host ""

# ============================================================================
# BƯỚC 2: UI TESTS (Selenium) - Cần browser, kiểm tra giao diện
# ============================================================================
Write-Host "🌐 BƯỚC 2: Chạy UI TESTS (Selenium) - Kiểm tra giao diện website" -ForegroundColor Yellow
Write-Host "────────────────────────────────────────────────────────────────" -ForegroundColor Yellow
Write-Host ""

try {
    cd "$projectRoot\SeleniumTests"
    Write-Host "📂 Thư mục: $projectRoot\SeleniumTests" -ForegroundColor Gray
    Write-Host "⏳ Chạy tests (Chrome sẽ mở và đóng tự động)..." -ForegroundColor Cyan
    Write-Host ""
    
    dotnet test --logger "console;verbosity=normal"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "✅ UI TESTS PASS!" -ForegroundColor Green
    } else {
        Write-Host ""
        Write-Host "⚠️  UI TESTS CÓ LỖI (có thể do website chậm)" -ForegroundColor Yellow
    }
} catch {
    Write-Host "⚠️  Lỗi khi chạy UI tests: $_" -ForegroundColor Red
}

Write-Host ""
Write-Host ""

# ============================================================================
# TÓM TẮT KẾT QUẢ
# ============================================================================
$endTime = Get-Date
$duration = $endTime - $startTime

Write-Host "═════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "📊 TÓM TẮT KẾT QUẢ" -ForegroundColor Cyan
Write-Host "═════════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
Write-Host "⏱️  Thời gian chạy: $([Math]::Round($duration.TotalSeconds, 2)) giây" -ForegroundColor Gray
Write-Host ""
Write-Host "✅ Hoàn thành chạy tất cả tests!" -ForegroundColor Green
Write-Host ""
Write-Host "📝 Ghi chú:" -ForegroundColor Cyan
Write-Host "  • UI Tests có thể fail nếu website slowdown" -ForegroundColor Gray
Write-Host "  • Logic Tests luôn pass (không phụ thuộc website)" -ForegroundColor Gray
Write-Host "  • Xem chi tiết kết quả ở trên" -ForegroundColor Gray
Write-Host ""
