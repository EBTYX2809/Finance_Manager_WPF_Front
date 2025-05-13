using Finance_Manager_WPF_Front.BackendApi;
using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class TransactionsViewModel : INotifyPropertyChanged
{
    public UserSession UserSession { get; set; }
    public ObservableCollection<CategoryModel> Categories { get; set; } = new ObservableCollection<CategoryModel>();
    private readonly TransactionsService _transactionsService;

    private bool _isTransactionEditorVisible;
    public bool IsTransactionEditorVisible
    {
        get => _isTransactionEditorVisible;
        set
        {
            if (_isTransactionEditorVisible != value)
            {
                _isTransactionEditorVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            _isEditMode = value;
            OnPropertyChanged(nameof(IsEditMode));
        }
    }

    private TransactionModel _newTransaction;
    public TransactionModel NewTransaction
    {
        get => _newTransaction;
        set
        {
            if (_newTransaction != value)
            {
                _newTransaction = value;
                OnPropertyChanged();
            }
        }
    }

    #region Date properties

    // Lists for ComboBoxes
    public List<int> DaysList { get; } = Enumerable.Range(1, 31).ToList();
    public List<string> MonthsList { get; } = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public List<int> YearsList { get; } = Enumerable.Range(DateTime.Now.Year - 5, 6).ToList(); // Current year and 5 years back
    public List<int> HoursList { get; } = Enumerable.Range(0, 24).ToList();
    public List<int> MinutesList { get; } = Enumerable.Range(0, 60).ToList();

    // Selected values
    private int _selectedDay = DateTime.Now.Day;
    public int SelectedDay
    {
        get => _selectedDay;
        set
        {
            _selectedDay = value;
            OnPropertyChanged(nameof(SelectedDay));
            UpdateTransactionDate();
        }
    }

    private string _selectedMonth = DateTime.Now.ToString("MMMM");
    public string SelectedMonth
    {
        get => _selectedMonth;
        set
        {
            _selectedMonth = value;
            OnPropertyChanged(nameof(SelectedMonth));
            UpdateTransactionDate();
        }
    }

    private int _selectedYear = DateTime.Now.Year;
    public int SelectedYear
    {
        get => _selectedYear;
        set
        {
            _selectedYear = value;
            OnPropertyChanged(nameof(SelectedYear));
            UpdateTransactionDate();
        }
    }

    private int _selectedHour = DateTime.Now.Hour;
    public int SelectedHour
    {
        get => _selectedHour;
        set
        {
            _selectedHour = value;
            OnPropertyChanged(nameof(SelectedHour));
            UpdateTransactionDate();
        }
    }

    private int _selectedMinute = DateTime.Now.Minute;
    public int SelectedMinute
    {
        get => _selectedMinute;
        set
        {
            _selectedMinute = value;
            OnPropertyChanged(nameof(SelectedMinute));
            UpdateTransactionDate();
        }
    }

    // Helper method to update the transaction date
    private void UpdateTransactionDate()
    {
        try
        {
            int monthIndex = MonthsList.IndexOf(SelectedMonth) + 1;
            DateTime newDate = new DateTime(SelectedYear, monthIndex, SelectedDay, SelectedHour, SelectedMinute, 0);
            NewTransaction.Date = newDate;
        }
        catch
        {
            // Handle invalid date (e.g., February 30)
            // You could set to a default date or leave as is
        }
    }

    #endregion

    public ICommand EditTransactionCommand { get; set; }
    public ICommand CloseTransactionEditorCommand { get; set; }
    public ICommand CreateTransactionCommand { get; set; }
    public ICommand GetTransactionsPageCommand { get; set; }
    public ICommand UpdateTransactionCommand { get; set; }    
    public ICommand DeleteTransactionCommand { get; set; }

    public TransactionsViewModel(UserSession userSession, TransactionsService transactionsService)
    {
        UserSession = userSession;
        _transactionsService = transactionsService;

        IsTransactionEditorVisible = false;
        IsEditMode = false;

        NewTransaction = new TransactionModel();

        EditTransactionCommand = new AsyncRelayCommand(EditTransactionAsync);
        CloseTransactionEditorCommand = new AsyncRelayCommand(CloseTransactionEditorAsync);
        CreateTransactionCommand = new AsyncRelayCommand(CreateTransactionAsync);
        GetTransactionsPageCommand = new AsyncRelayCommand(GetTransactionsPageAsync);
        UpdateTransactionCommand = new AsyncRelayCommand(UpdateTransactionAsync);
        DeleteTransactionCommand = new AsyncRelayCommand(DeleteTransactionAsync);
    }   

    private async Task EditTransactionAsync(object parameter)
    {
        IsTransactionEditorVisible = true;

        if (parameter is TransactionModel transaction)
        {
            NewTransaction = transaction;
            IsEditMode = true;
        }
        else IsEditMode = false;
    }

    private async Task CloseTransactionEditorAsync(object parameter)
    {
        IsTransactionEditorVisible = false;
        NewTransaction = null;
    }

    private async Task CreateTransactionAsync(object parameter)
    {
        await _transactionsService.CreateTransactionAsync(NewTransaction);
        CloseTransactionEditorAsync(null);
    }

    private async Task GetTransactionsPageAsync(object parameter)
    {
        await _transactionsService.GetTransactionsPageAsync();
    }

    private async Task UpdateTransactionAsync(object parameter)
    {
        await _transactionsService.UpdateTransactionAsync(NewTransaction); // типо мы новую создаем, отличающуюся от старой
        CloseTransactionEditorAsync(null);
    }

    private async Task DeleteTransactionAsync(object parameter)
    {
        if (parameter is TransactionModel transaction)
            await _transactionsService.DeleteTransactionAsync(transaction);
    }

    public void LoadCategories()
    {
        //Categories = CategoriesStorage.AllCategories.Where(c => c.InnerCategories != null) as ObservableCollection<CategoryModel>;
        foreach(var category in CategoriesStorage.AllCategories) Categories.Add(category);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}