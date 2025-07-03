@echo off
netsparkle-generate-appcast ^
  --appcast-output-directory C:/out ^
  --binaries C:/app/publish ^
  --single-file C:/app/FinanceManagerWPF.zip ^
  --ext zip ^
  --os windows-x64 ^
  --base-url %APP_BASE_URL% ^
  --product-name FinanceManagerWPF ^
  --human-readable true ^
  --output-file-name appcast