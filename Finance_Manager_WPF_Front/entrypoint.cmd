@echo off
netsparkle-generate-appcast ^
  --appcast-output-directory C:/out ^
  --binaries C:/app/publish ^
REM  --single-file C:/app/FinanceManagerWPF.zip ^
  --ext exe ^
  --os windows-x64 ^
  --base-url %APP_BASE_URL% ^
  --product-name FinanceManagerWPF ^
  --human-readable true ^
  --output-file-name appcast