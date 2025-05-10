using System.Net.Http;
using System.Windows;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;

namespace Finance_Manager_WPF_Front.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly UserSession _userSession;
    private readonly UserService _userService;
    private readonly CategoriesService _categoriesService;
    private readonly TransactionsService _transactionsService;
    private readonly SavingsService _savingsService;
    public MainWindow(UserSession userSession, UserService userService, CategoriesService categoriesService, TransactionsService transactionsService, SavingsService savingsService)
    {
        InitializeComponent();
        _userSession = userSession;
        _userService = userService;
        _categoriesService = categoriesService;
        _transactionsService = transactionsService;
        _savingsService = savingsService;

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadStartupUserData();
        MessageBox.Show($"I'm here: {_userSession.CurrentUser?.Email}.");
    }

    private async Task LoadStartupUserData()
    {
        await _userService.UpdateUserBalanceAsync();
        await _categoriesService.LoadAllCategoriesAsync();
        await _transactionsService.GetTransactionsPageAsync();
        await _savingsService.GetSavingsPageAsync();       
    }
}