using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using Finance_Manager_WPF_Front.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class PrimaryCurrencyPickViewModel : INotifyPropertyChanged
{
    private readonly UserService _userService;
    private readonly WindowChanger _windowChanger;

    private string _selectedCurrencyCode;
    public string SelectedCurrencyCode
    {
        get => _selectedCurrencyCode;
        set
        {
            if (_selectedCurrencyCode != value)
            {
                _selectedCurrencyCode = value;
                OnPropertyChanged();
            }
        }
    }
    public List<string> CurrencyCodes { get; set; }

    public ICommand AcceptCommand { get; set; }

    public PrimaryCurrencyPickViewModel(UserService userService, WindowChanger windowChanger)
    {
        _userService = userService;
        _windowChanger = windowChanger;

        CurrencyCodes = CurrencyCultureProvider.GetCurrenciesList();

        AcceptCommand = new AsyncRelayCommand(AcceptAsync);
    }

    private async Task AcceptAsync(object arg)
    {
        await _userService.AddCurrencyAsync("Primary", SelectedCurrencyCode);
        _windowChanger.GoToMainWindow();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
