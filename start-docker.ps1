# Start Docker Desktop
Write-Host "Starting Docker Desktop..." -ForegroundColor Cyan

# Try to find Docker Desktop
$dockerExe = @(
    "C:\Program Files\Docker\Docker\Docker.exe",
    "C:\Program Files\Docker\Docker\frontend\Docker.exe",
    "$env:ProgramFiles\Docker\Docker\Docker.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1

if ($dockerExe) {
    Write-Host "Found Docker at: $dockerExe" -ForegroundColor Green
    Start-Process $dockerExe
    Write-Host "Docker Desktop is starting..." -ForegroundColor Yellow
    Write-Host "Please wait 30-60 seconds for the daemon to be ready" -ForegroundColor Yellow
} else {
    Write-Host "Docker Desktop executable not found!" -ForegroundColor Red
    Write-Host "Please manually start Docker Desktop from Start Menu" -ForegroundColor Magenta
}
