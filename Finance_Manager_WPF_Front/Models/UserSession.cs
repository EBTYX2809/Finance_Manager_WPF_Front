using Finance_Manager_WPF_Front.Services.AuthServices;
using Finance_Manager_WPF_Front.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Finance_Manager_WPF_Front.Models;

public class UserSession : INotifyPropertyChanged
{
    private UserModel _currentUser;
    public UserModel CurrentUser
    {
        get => _currentUser;
        set
        {
            if(value != _currentUser)
            {
                _currentUser = value;
            }
        }
    }
    public string? AccessToken { get; set; }

    public bool IsAuthenticated => CurrentUser != null;

    public Action LogoutAction;
    public void Logout()
    {
        CurrentUser = null;
        AccessToken = null;
        LogoutAction?.Invoke();        
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}