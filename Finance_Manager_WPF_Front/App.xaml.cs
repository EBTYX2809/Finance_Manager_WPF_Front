using Finance_Manager_WPF_Front.Views;
using Finance_Manager_WPF_Front.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Windows;
using System.IO;
using Finance_Manager_WPF_Front.BackendApi;
using System.Net.Http;

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

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<UserSession>();

            services.AddSingleton<HttpClient>();

            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<TransactionsViewModel>();
            services.AddSingleton<AnalyticsViewModel>();
            services.AddSingleton<SavingsViewModel>();
            services.AddSingleton<PlanningViewModel>();
            services.AddSingleton<SettingsViewModel>();

            services.AddSingleton<LoginWindow>();
            services.AddSingleton<MainWindow>();
        }      
    }
}
