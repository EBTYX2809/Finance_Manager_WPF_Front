using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance_Manager_WPF_Front.Models;

public class SavingModel : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private decimal _goal;
    private decimal? _currentAmount;

    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal Goal
    {
        get => _goal;
        set
        {
            if (_goal != value)
            {
                _goal = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal? CurrentAmount
    {
        get => _currentAmount;
        set
        {
            if (_currentAmount != value)
            {
                _currentAmount = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}