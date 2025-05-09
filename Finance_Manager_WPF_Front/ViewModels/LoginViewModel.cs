using Finance_Manager_WPF_Front.Services.AuthServices;
using Finance_Manager_WPF_Front.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthService _authService;
    private readonly TokensManager _tokensManager;
    private readonly WindowChanger _windowChanger;

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            if (value != _email)
            {
                _email = value;
                OnPropertyChanged();
                if (_email.Length > 0) IsEmailPlaceholderVisible = false;
                else IsEmailPlaceholderVisible = true;
            }
        }
    }

    private bool _isEmailPlaceholderVisible;
    public bool IsEmailPlaceholderVisible
    {
        get => _isEmailPlaceholderVisible;
        set
        {
            if (value != _isEmailPlaceholderVisible)
            {
                _isEmailPlaceholderVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isPasswordPlaceholderVisible;
    public bool IsPasswordPlaceholderVisible
    {
        get => _isPasswordPlaceholderVisible;
        set
        {
            if (value != _isPasswordPlaceholderVisible) 
            {
                _isPasswordPlaceholderVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public SecureString SecurePassword { get; set; }

    public ICommand RegisterCommand { get; set; }
    public ICommand AuthCommand { get; set; }
    public ICommand AutoLoginCommand { get; set; }

    public LoginViewModel(AuthService authService, TokensManager tokensManager, WindowChanger windowChanger)
    {
        _authService = authService;
        _tokensManager = tokensManager;
        _windowChanger = windowChanger;

        RegisterCommand = new AsyncRelayCommand(RegisterAsync);
        AuthCommand = new AsyncRelayCommand(AuthAsync);
        AutoLoginCommand = new AsyncRelayCommand(AutoLoginAsync);

        IsEmailPlaceholderVisible = true;
        IsPasswordPlaceholderVisible = true;
        AutoLoginCommand.Execute(null);        
    }

    private async Task AutoLoginAsync(object parameter)
    {
        var existToken = _tokensManager.LoadRefreshToken();

        if (existToken == null) return;        
        
        await _authService.LoginUserWithRefreshToken(existToken);
        _windowChanger.GoToMainWindow();        
    }

    private async Task AuthAsync(object parameter)
    {
        if (!ValidateCredentials()) return;
        await _authService.AuthUser(Email, SecurePassword);
        _windowChanger.GoToMainWindow();
    }

    private async Task RegisterAsync(object parameter)
    {
        if(!ValidateCredentials()) return;
        await _authService.RegisterUser(Email, SecurePassword);
        _windowChanger.GoToMainWindow();
    }

    private bool ValidateCredentials()
    {
        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains('@') || !Email.Contains('.'))
        {
            MessageBox.Show("Invalid email.");
            return false;
        }

        if (SecurePassword.Length < 6)
        {
            MessageBox.Show("Password must be at least 6 characters.");
            return false;
        }

        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
