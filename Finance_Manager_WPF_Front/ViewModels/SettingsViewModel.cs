using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{
    private readonly UserSession _userSession;
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

    private bool _isCurrencyEditorVisible;
    public bool IsCurrencyEditorVisible
    {
        get => _isCurrencyEditorVisible;
        set
        {
            if (_isCurrencyEditorVisible != value) 
            {
                _isCurrencyEditorVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand LogoutCommand { get; set; }
    public ICommand OpenCurrencyEditorCommand { get; set; }
    public ICommand CloseCurrencyEditorCommand { get; set; }
    public ICommand AddCurrencyCommand { get; set; }

    public SettingsViewModel(UserSession userSession, UserService userService)
    {
        _userSession = userSession;
        _userService = userService;

        IsCurrencyEditorVisible = false;

        LogoutCommand = new AsyncRelayCommand(LogoutAsync);
        AddCurrencyCommand = new AsyncRelayCommand(AddCurrencyAsync);
        OpenCurrencyEditorCommand = new AsyncRelayCommand(OpenCurrencyEditorAsync);
        CloseCurrencyEditorCommand = new AsyncRelayCommand(CloseCurrencyEditorAsync);
    }


    private async Task OpenCurrencyEditorAsync(object parameter)
    {
        IsCurrencyEditorVisible = true;
    }

    private async Task CloseCurrencyEditorAsync(object parameter)
    {
        IsCurrencyEditorVisible = false;
        NewCurrencyCode = null;
        NewCurrencyRang = null;
    }

    private async Task LogoutAsync(object parameter)
    {
        _userSession.Logout();
    }

    private async Task AddCurrencyAsync(object parameter)
    {
        await _userService.AddCurrencyAsync(NewCurrencyRang, NewCurrencyCode);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}