$countPass = (Get-ChildItem -File -Path ".\premade-programs\pass\" | Measure-Object).Count
$countFail = (Get-ChildItem -File -Path ".\premade-programs\fail\" | Measure-Object).Count
$exePath = "$TRAVIS_BUILD_DIR\Core\bin\Debug\netcoreapp3.1\"
$pathBack = "$TRAVIS_BUILD_DIR"
function ExpectError() {
    param(
        $exitcode,
        $id
    )
    if ($exitcode -eq 0) {
        Write-Host "[file: $id.pi ($exitcode)] Compiler compiled when it should not. Exiting" -ForegroundColor Red
        Set-Location "$pathBack"
        exit -1
    }
}
function ExpectNoError() {
    param(
        $exitcode,
        $id
    )
    if ($exitcode -ne 0) {
        Write-Host "[file: $id.pi ($exitcode)] Compiler did not compile when it should. Exiting" -ForegroundColor Red
        Set-Location "$pathBack"
        exit -1
    }
}
Set-Location $exePath
.\Core.exe
Write-Host "Checking passing compilations" -ForegroundColor Green
for ($i = 1; $i -lt $countPass; $i++) {
    $proc = Start-Process -FilePath ".\Core.exe" -ArgumentList "$pathBack\premade-programs\pass\$i.pi -v" -WorkingDirectory "." -NoNewWindow -PassThru

    $hasTimeout = $null
    $proc | Wait-Process -Timeout 4 -ErrorAction SilentlyContinue -ErrorVariable hasTimeout

    if ($hasTimeout) {
        Write-Host "[file: $id.pi] Compiler did not compile when it should. Exiting" -ForegroundColor Red
        $proc | Stop-Process
    }
    ExpectNoError -exitcode $proc.ExitCode -id $i
}
Write-Host "Checking failing compilations" -ForegroundColor Green
for ($i = 1; $i -lt $countFail; $i++) {
    $proc = Start-Process -FilePath ".\Core.exe" -ArgumentList "$pathBack\premade-programs\fail\$i.pi -v" -WorkingDirectory "." -NoNewWindow  -PassThru
    
    $hasTimeout = $null
    $proc | Wait-Process -Timeout 4 -ErrorAction SilentlyContinue -ErrorVariable hasTimeout
    
    if ($hasTimeout) {
        Write-Host "[file: $id.pi] Compiler compiled when it should not. Exiting" -ForegroundColor Red
        $proc | Stop-Process
    }
    ExpectError -exitcode $proc.ExitCode -id $i
}
Set-Location $pathBack
Write-Host "All tests passed successfully!" -ForegroundColor Green