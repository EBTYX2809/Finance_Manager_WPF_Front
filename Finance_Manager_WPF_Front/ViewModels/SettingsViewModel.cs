using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using Finance_Manager_WPF_Front.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{
    public UserSession _userSession { get; set; }
    private readonly UserService _userService;

    private string _newCurrencyRang;
    public string NewCurrencyRang
    {
        get => _newCurrencyRang;
        set
        {
            if (_newCurrencyRang != value)
            {
                _newCurrencyRang = value;
                OnPropertyChanged();
            }
        }
    }

    private string _newCurrencyCode;
    public string NewCurrencyCode
    {
        get => _newCurrencyCode;
        set
        {
            if(_newCurrencyCode != value)
            {
                _newCurrencyCode = value;
                OnPropertyChanged();
            }
        }
    }   

    public List<string> CurrencyRangs { get; set; } = new List<string>
    {
        "Secondary1",
        "Secondary2"
    };

    public List<string> CurrencyCodes { get; set; }

    public ICommand LogoutCommand { get; set; }
    public ICommand AddCurrencyCommand { get; set; }

    public SettingsViewModel(UserSession userSession, UserService userService)
    {
        _userSession = userSession;
        _userService = userService;

        CurrencyCodes = CurrencyCultureProvider.GetCurrenciesList();

        LogoutCommand = new AsyncRelayCommand(LogoutAsync);
        AddCurrencyCommand = new AsyncRelayCommand(AddCurrencyAsync);
    }

    private async Task LogoutAsync(object parameter)
    {
        _userSession.Logout();
    }

    private async Task AddCurrencyAsync(object parameter)
    {
        await _userService.AddCurrencyAsync(NewCurrencyRang, NewCurrencyCode);
        NewCurrencyCode = null;
        NewCurrencyRang = null;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}