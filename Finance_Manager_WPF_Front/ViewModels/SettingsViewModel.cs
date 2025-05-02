using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finance_Manager_WPF_Front.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}