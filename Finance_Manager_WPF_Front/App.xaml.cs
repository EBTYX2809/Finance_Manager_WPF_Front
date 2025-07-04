﻿using Finance_Manager_WPF_Front.Views;
using Finance_Manager_WPF_Front.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Windows;
using System.IO;
using Finance_Manager_WPF_Front.BackendApi;
using System.Net.Http;
using Finance_Manager_WPF_Front.Services;
using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services.AuthServices;
using Serilog;
using Microsoft.Extensions.Logging;
using NetSparkleUpdater;
using NetSparkleUpdater.UI.WPF;
using NetSparkleUpdater.SignatureVerifiers;

namespace Finance_Manager_WPF_Front
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Config { get; private set; }
        private SparkleUpdater _updater;
        protected override void OnStartup(StartupEventArgs e)
        {
            // Services
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            CurrencyCultureProvider.Initialize(ServiceProvider.GetRequiredService<UserSession>());

            // NetSparkle auto updater
            _updater = new SparkleUpdater(
                Config["AppcastUrl"],
                new Ed25519Checker(NetSparkleUpdater.Enums.SecurityMode.Strict,
                    publicKey: Config["SparklePublicKey"]))
            {
                UIFactory = new UIFactory(),
                RelaunchAfterUpdate = true
            };
            _updater.StartLoop(true);

            // Start from Login Window
            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            /*var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();*/
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Config
            SetConfig();

            // Helpers
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSingleton<UserSession>();

            // API
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ApiWrapper>();
            services.AddSingleton<FinanceApiClient>(sp =>
            {
                var baseUrl = Config["BackendUri"];
                var httpClient = sp.GetRequiredService<HttpClient>();
                var tokenManager = sp.GetRequiredService<TokensManager>();
                return new FinanceApiClient(baseUrl, httpClient, tokenManager);
            });

            // Services
            services.AddSingleton<AuthService>();
            services.AddSingleton<TokensManager>();
            services.AddSingleton<UserService>();
            services.AddSingleton<CategoriesService>();
            services.AddSingleton<TransactionsService>();
            services.AddSingleton<SavingsService>();
            services.AddSingleton<AnalyticsService>();

            // ViewModels
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<TransactionsViewModel>();
            services.AddSingleton<AnalyticsViewModel>();
            services.AddSingleton<SavingsViewModel>();
            //services.AddSingleton<PlanningViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<PrimaryCurrencyPickViewModel>();

            // Views
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();
            services.AddTransient<PrimaryCurrencyPickWindow>();
            services.AddTransient<WindowChanger>();
        }

        private void SetConfig()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            if (env == "Production")
            {
                var password = Environment.GetEnvironmentVariable("APPSETTINGS_PASSWORD");

                if (string.IsNullOrWhiteSpace(password))
                    return;

                using var encryptedStream = File.OpenRead("appsettings.enc");
                using var decryptedStream = Decryptor.DecryptWithOpenSsl(encryptedStream, password);

                Config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonStream(decryptedStream)
                    .Build();
            }
            else
            {
                Config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            }

        }
    }
}
