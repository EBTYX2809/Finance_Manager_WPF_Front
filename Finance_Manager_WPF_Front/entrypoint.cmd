@echo off
netsparkle-generate-appcast ^
  --appcast-output-directory C:/out ^
  --single-file C:/app/FinanceManagerWPF.zip ^
  --ext zip ^
  --file-version %APP_VERSION% ^
  --os windows-x64 ^
  --base-url %APP_BASE_URL% ^
  --product-name FinanceManagerWPF ^
  --human-readable true ^
  --output-file-name appcast