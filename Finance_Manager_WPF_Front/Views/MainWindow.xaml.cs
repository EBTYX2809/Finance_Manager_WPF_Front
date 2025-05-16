using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using Finance_Manager_WPF_Front.ViewModels;

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
    private readonly TransactionsViewModel _transactionsViewModel;
    private readonly SavingsViewModel _savingsViewModel;
    private readonly AnalyticsViewModel _analyticsViewModel;
    private readonly SettingsViewModel _settingsViewModel;
    public MainWindow(UserSession userSession, UserService userService, CategoriesService categoriesService, TransactionsService transactionsService, SavingsService savingsService, TransactionsViewModel transactionsViewModel, SavingsViewModel savingsViewModel, AnalyticsViewModel analyticsViewModel, SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        _userSession = userSession;
        _userService = userService;
        _categoriesService = categoriesService;
        _transactionsService = transactionsService;
        _savingsService = savingsService;

        _transactionsViewModel = transactionsViewModel;
        _savingsViewModel = savingsViewModel;
        _analyticsViewModel = analyticsViewModel;
        _settingsViewModel = settingsViewModel;

        Loaded += MainWindow_Loaded;
        //TransactionsPageButton_Click(null, null);
        //SavingsPageButton_Click(null, null);
        //AnalyticsPageButton_Click(null, null);
        SettigsPageButton_Click(null, null);
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadStartupUserData();
        _transactionsViewModel.LoadCategories();
        _savingsViewModel.UpdateSumSavings();
        _analyticsViewModel.GetAnalyticsForWeekCommand.Execute(null);
    }

    private async Task LoadStartupUserData()
    {
        await _userService.UpdateUserBalanceAsync();
        await _categoriesService.LoadAllCategoriesAsync();
        await _transactionsService.GetTransactionsPageAsync();
        await _savingsService.GetSavingsPageAsync();
    }

    private void TransactionsPageButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext = _transactionsViewModel;
        HidePages();
        TransactionsPage.Visibility = Visibility.Visible;
    }

    private void SavingsPageButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext = _savingsViewModel;
        HidePages();
        SavingsPage.Visibility = Visibility.Visible;
    }

    private void AnalyticsPageButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext = _analyticsViewModel;
        HidePages();
        AnalyticsPage.Visibility = Visibility.Visible;
    }

    private void SettigsPageButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext = _settingsViewModel;
        HidePages();
        SettingsPage.Visibility = Visibility.Visible;
    }

    private void HidePages()
    {
        TransactionsPage.Visibility = Visibility.Collapsed;
        SavingsPage.Visibility = Visibility.Collapsed;
        AnalyticsPage.Visibility = Visibility.Collapsed;
        SettingsPage.Visibility = Visibility.Collapsed;
    }
}