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
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            /*var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();*/

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
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

            // ViewModels
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<TransactionsViewModel>();
            services.AddSingleton<AnalyticsViewModel>();
            services.AddSingleton<SavingsViewModel>();
            services.AddSingleton<PlanningViewModel>();
            services.AddSingleton<SettingsViewModel>();

            // Views
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<WindowChanger>();
        }      
    }
}
