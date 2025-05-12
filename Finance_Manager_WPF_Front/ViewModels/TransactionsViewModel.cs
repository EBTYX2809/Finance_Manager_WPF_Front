using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class TransactionsViewModel : INotifyPropertyChanged
{
    private readonly UserSession _userSession;
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

    private bool _isTransactionVisible;
    public bool IsTransactionVisible
    {
        get => _isTransactionVisible;
        set
        {
            if (_isTransactionVisible != value)
            {
                _isTransactionVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private TransactionModel _selectedTransaction;
    public TransactionModel SelectedTransaction
    {
        get => _selectedTransaction;
        set
        {
            if (_selectedTransaction != value)
            {
                _selectedTransaction = value;
                OnPropertyChanged();
            }
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

    public ICommand OpenTransactionCommand { get; set; }
    public ICommand CloseTransactionCommand { get; set; }
    public ICommand EditTransactionCommand { get; set; }
    public ICommand CloseTransactionEditorCommand { get; set; }
    public ICommand CreateTransactionCommand { get; set; }
    public ICommand GetTransactionsPageCommand { get; set; }
    public ICommand UpdateTransactionCommand { get; set; }    
    public ICommand DeleteTransactionCommand { get; set; }

    public TransactionsViewModel(UserSession userSession, TransactionsService transactionsService)
    {
        _userSession = userSession;
        _transactionsService = transactionsService;

        IsTransactionEditorVisible = false;
        IsTransactionVisible = false;

        SelectedTransaction = new TransactionModel();
        NewTransaction = new TransactionModel();

        OpenTransactionCommand = new AsyncRelayCommand(OpenTransactionAsync);
        CloseTransactionCommand = new AsyncRelayCommand(CloseTransactionAsync);
        EditTransactionCommand = new AsyncRelayCommand(EditTransactionAsync);
        CloseTransactionEditorCommand = new AsyncRelayCommand(CloseTransactionEditorAsync);
        CreateTransactionCommand = new AsyncRelayCommand(CreateTransactionAsync);
        GetTransactionsPageCommand = new AsyncRelayCommand(GetTransactionsPageAsync);
        UpdateTransactionCommand = new AsyncRelayCommand(UpdateTransactionAsync);
        DeleteTransactionCommand = new AsyncRelayCommand(DeleteTransactionAsync);        
    }

    private async Task OpenTransactionAsync(object parameter)
    {
        IsTransactionVisible = true;
    }

    private async Task CloseTransactionAsync(object parameter)
    {
        IsTransactionVisible = false;
        SelectedTransaction = null;
    }

    private async Task EditTransactionAsync(object parameter)
    {
        IsTransactionEditorVisible = true;
        IsTransactionVisible = false;
        if(SelectedTransaction != null) NewTransaction = SelectedTransaction;
    }

    private async Task CloseTransactionEditorAsync(object parameter)
    {
        IsTransactionEditorVisible = false;
        NewTransaction = null;
    }

    private async Task CreateTransactionAsync(object parameter)
    {
        await _transactionsService.CreateTransactionAsync(NewTransaction);
    }

    private async Task GetTransactionsPageAsync(object parameter)
    {
        await _transactionsService.GetTransactionsPageAsync();
    }

    private async Task UpdateTransactionAsync(object parameter)
    {
        await _transactionsService.UpdateTransactionAsync(NewTransaction); // типо мы новую создаем, отличающуюся от старой
    }

    private async Task DeleteTransactionAsync(object parameter)
    {
        await _transactionsService.DeleteTransactionAsync(SelectedTransaction);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}