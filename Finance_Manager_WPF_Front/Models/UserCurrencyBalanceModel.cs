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
            decimal truncated = Math.Floor(value * 100) / 100;
            if (_balance != truncated)
            {
                _balance = truncated;
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
