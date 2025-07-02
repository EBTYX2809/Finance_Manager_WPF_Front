@echo off
netsparkle-generate-appcast ^
  --appcast-output-directory C:/out ^
  --binaries C:/app/publish ^
  --ext exe ^
  --os windows-x64 ^
  --product-name FinanceManagerWPF ^
  --file-version %APP_VERSION% ^
  --human-readable true ^
  --output-file-name appcast