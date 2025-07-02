@echo off
netsparkle-generate-appcast ^
  --appcast-output-directory C:/out ^
  --binaries C:/app/publish ^
  --ext exe ^
  --os windows-x64 ^
  --product-name FinanceManagerWPF ^
  --file-version %APP_VERSION% ^
  --key-path C:/keys ^
  --human-readable true ^
  --output-file-name appcast