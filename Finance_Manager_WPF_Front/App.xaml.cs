using Finance_Manager_WPF_Front.Views;
using Finance_Manager_WPF_Front.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Windows;
using System.IO;
using Finance_Manager_WPF_Front.BackendApi;
using System.Net.Http;
using Finance_Manager_WPF_Front.Services;
using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services.AuthServices;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Finance_Manager_WPF_Front
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Config { get; private set; }

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

            // Start from Login Window
            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            /*var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();*/
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Helpers
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSingleton<UserSession>();

            // API
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ApiWrapper>();
            services.AddSingleton<FinanceApiClient>(sp =>
            {
                var baseUrl = "https://localhost:49464";
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
    }
}
