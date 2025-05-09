using Finance_Manager_WPF_Front.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Finance_Manager_WPF_Front.Views;

public class WindowChanger
{
    private readonly IServiceProvider _provider;
    private readonly UserSession _userSession;
    public WindowChanger(IServiceProvider provider, UserSession userSession)
    {
        _provider = provider;
        _userSession = userSession;
        _userSession.LogoutAction += GoToLoginWindow;
    }

    public void GoToMainWindow()
    {
        Window currentWindow = Application.Current.MainWindow;
        var _mainWindow = _provider.GetRequiredService<MainWindow>();
        Application.Current.MainWindow = _mainWindow;
        _mainWindow.Show();
        currentWindow?.Close();
    }

    public void GoToLoginWindow() 
    {
        var currentWindow = Application.Current.MainWindow;
        var _loginWindow = _provider.GetRequiredService<LoginWindow>();
        Application.Current.MainWindow = _loginWindow;
        _loginWindow.Show();
        currentWindow?.Close();
    }
}
