using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_WPF_Front.Models;

public class UserCurrencyBalanceModel : INotifyPropertyChanged
{
    private string _currency;
    public string Currency
    {
        get => _currency;
        set
        {
            if (_currency != value)
            {
                _currency = value;
                OnPropertyChanged();
            }
        }
    }

    private decimal _balance;
    public decimal Balance
    {
        get => _balance;
        set
        {
            if (_balance != value)
            {
                _balance = value;
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
