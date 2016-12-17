@echo off
cls

IF NOT EXIST .paket\paket.exe .paket\paket.bootstrapper.exe

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe generate-include-scripts
if errorlevel 1 (
  exit /b %errorlevel%
)

pause