using System.Net.Http;
using System.Windows;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;

namespace Finance_Manager_WPF_Front.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly HttpClient _httpClient;
    public MainWindow(/*HttpClient httpClient*/)
    {
        InitializeComponent();
        
        _httpClient = new HttpClient(); // httpClient

        MainTest();
    }

    public async Task MainTest()
    {
        var apiClient = new FinanceApiClient("https://localhost:49464", _httpClient);

        var categories = await apiClient.CategoriesAllAsync();

        string names = string.Empty;

        foreach (var category in categories) 
        {
            names += $"Name: {category.Name}\n";
        }

        MessageBox.Show(names);
    }
}