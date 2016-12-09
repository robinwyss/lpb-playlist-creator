@echo off
cls


.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe generate-include-script
if errorlevel 1 (
  exit /b %errorlevel%
)

pause