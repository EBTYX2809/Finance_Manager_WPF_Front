@echo off
setlocal
set ZIP_FILE=FinanceManagerWPF_%APP_VERSION%.zip
netsparkle-generate-appcast ^
  --appcast-output-directory C:/out ^
  --single-file C:/app/%ZIP_FILE% ^
  --ext zip ^
  --os windows-x64 ^
  --base-url %APP_BASE_URL% ^
  --product-name FinanceManagerWPF ^
  --human-readable true ^
  --output-file-name appcast