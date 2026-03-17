# Docker Setup & Diagnostic Script
# Run as Administrator

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Docker Setup & Diagnostics" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

# Step 1: Check Docker Installation
Write-Host "[1] Checking Docker Installation..." -ForegroundColor Yellow
$dockerVersion = docker --version 2>$null
if ($dockerVersion) {
    Write-Host "✓ Docker is installed: $dockerVersion" -ForegroundColor Green
} else {
    Write-Host "✗ Docker is NOT installed" -ForegroundColor Red
    Write-Host "  Download from: https://www.docker.com/products/docker-desktop" -ForegroundColor Magenta
    exit
}

# Step 2: Check WSL2
Write-Host "`n[2] Checking WSL2 Installation..." -ForegroundColor Yellow
$wslCheck = wsl.exe -l -v 2>$null
if ($wslCheck) {
    Write-Host "✓ WSL2 is installed:" -ForegroundColor Green
    Write-Host $wslCheck
} else {
    Write-Host "⚠ WSL2 may not be installed. Docker Desktop requires WSL2" -ForegroundColor Yellow
}

# Step 3: Check Docker Daemon Status
Write-Host "`n[3] Checking Docker Daemon Status..." -ForegroundColor Yellow
$dockerPs = docker ps 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Docker daemon is RUNNING" -ForegroundColor Green
    Write-Host "  Containers: $($dockerPs.Count)" -ForegroundColor Cyan
} else {
    Write-Host "✗ Docker daemon is NOT running" -ForegroundColor Red
    Write-Host "  Error: $dockerPs" -ForegroundColor Yellow
    Write-Host "`n  SOLUTION: Start Docker Desktop" -ForegroundColor Magenta
    Write-Host "  1. Click Start Menu" -ForegroundColor White
    Write-Host "  2. Type 'Docker Desktop'" -ForegroundColor White
    Write-Host "  3. Press Enter and wait 30-60 seconds" -ForegroundColor White
    Write-Host "  4. Check system tray for Docker icon" -ForegroundColor White
}

# Step 4: Check Docker Images
Write-Host "`n[4] Checking Existing Docker Images..." -ForegroundColor Yellow
$images = docker images 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Docker images available:" -ForegroundColor Green
    docker images --format "table {{.Repository}}\t{{.Tag}}\t{{.Size}}"
} else {
    Write-Host "✗ Cannot list images" -ForegroundColor Red
}

# Step 5: Verify Project Structure
Write-Host "`n[5] Checking Project Structure..." -ForegroundColor Yellow
$projectRoot = Get-Location

if (Test-Path "$projectRoot\Dockerfile") {
    Write-Host "✓ Backend Dockerfile" -ForegroundColor Green
} else {
    Write-Host "✗ Backend Dockerfile - NOT FOUND" -ForegroundColor Red
}

if (Test-Path "$projectRoot\docker-compose.yml") {
    Write-Host "✓ docker-compose.yml" -ForegroundColor Green
} else {
    Write-Host "✗ docker-compose.yml - NOT FOUND" -ForegroundColor Red
}

if (Test-Path "$projectRoot\employee-management-ui\Dockerfile") {
    Write-Host "✓ Frontend Dockerfile" -ForegroundColor Green
} else {
    Write-Host "✗ Frontend Dockerfile - NOT FOUND" -ForegroundColor Red
}

if (Test-Path "$projectRoot\employee-management-ui\nginx.conf") {
    Write-Host "✓ nginx.conf" -ForegroundColor Green
} else {
    Write-Host "✗ nginx.conf - NOT FOUND" -ForegroundColor Red
}

# Step 6: Show Next Steps
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "NEXT STEPS:" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✓ All checks passed! Ready to build Docker images.`n" -ForegroundColor Green
    Write-Host "To build and run containers, use:" -ForegroundColor Yellow
    Write-Host "  1. docker-compose up       # Start all containers" -ForegroundColor Cyan
    Write-Host "  2. Access frontend:        http://localhost" -ForegroundColor Cyan
    Write-Host "  3. Access backend:         http://localhost:5000" -ForegroundColor Cyan
    Write-Host "  4. Access API docs:        http://localhost:5000/swagger" -ForegroundColor Cyan
} else {
    Write-Host "`n✗ Docker is not running. Please start Docker Desktop first.`n" -ForegroundColor Red
    Write-Host "Windows Start Menu → Search 'Docker Desktop' → Click to open" -ForegroundColor Magenta
    Write-Host "Wait 30-60 seconds for startup, then run this script again." -ForegroundColor White
}

Write-Host "`n========================================`n" -ForegroundColor Cyan
