using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance_Manager_WPF_Front.Models;

public class UserModel : INotifyPropertyChanged
{
    private string _email = string.Empty;
    private UserCurrencyBalanceModel _primaryCurrencyBalance;
    private UserCurrencyBalanceModel _secondaryCurrencyBalance1;
    private UserCurrencyBalanceModel _secondaryCurrencyBalance2;
    private ObservableCollection<TransactionModel> _transactions = new();
    private ObservableCollection<SavingModel> _savings = new();

    public int Id { get; set; }

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }
   
    public UserCurrencyBalanceModel PrimaryCurrencyBalance
    {
        get => _primaryCurrencyBalance;
        set
        {
            if (_primaryCurrencyBalance != value) 
            {
                _primaryCurrencyBalance = value;
                OnPropertyChanged();
            }
        }
    }

    public UserCurrencyBalanceModel SecondaryCurrencyBalance1
    {
        get => _secondaryCurrencyBalance1;
        set
        {
            if (_secondaryCurrencyBalance1 != value)
            {
                _secondaryCurrencyBalance1 = value;
                OnPropertyChanged();
            }
        }
    }

    public UserCurrencyBalanceModel SecondaryCurrencyBalance2
    {
        get => _secondaryCurrencyBalance2;
        set
        {
            if (_secondaryCurrencyBalance2 != value)
            {
                _secondaryCurrencyBalance2 = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<TransactionModel> Transactions
    {
        get => _transactions;
        set
        {
            if (_transactions != value)
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<SavingModel> Savings
    {
        get => _savings;
        set
        {
            if (_savings != value)
            {
                _savings = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}