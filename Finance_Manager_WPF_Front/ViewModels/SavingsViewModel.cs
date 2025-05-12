using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class SavingsViewModel : INotifyPropertyChanged
{
    private readonly UserSession _userSession;
    private readonly SavingsService _savingsService;

    private bool _isTopUpEditorVisible;
    public bool IsTopUpEditorVisible
    {
        get => _isTopUpEditorVisible;
        set
        {
            if (_isTopUpEditorVisible != value)
            {
                _isTopUpEditorVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isSavingEditorVisible;
    public bool IsSavingEditorVisible
    {
        get => _isSavingEditorVisible;
        set
        {
            if (_isSavingEditorVisible != value)
            {
                _isSavingEditorVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isSavingVisible;
    public bool IsSavingVisible
    {
        get => _isSavingVisible;
        set
        {
            if (_isSavingVisible != value)
            {
                _isSavingVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private SavingModel _selectedSaving;
    public SavingModel SelectedSaving
    {
        get => _selectedSaving;
        set
        {
            if (_selectedSaving != value)
            {
                _selectedSaving = value;
                OnPropertyChanged();
            }
        }
    }

    private SavingModel _newSaving;
    public SavingModel NewSaving
    {
        get => _newSaving;
        set
        {
            if (_newSaving != value)
            {
                _newSaving = value;
                OnPropertyChanged();
            }
        }
    }

    private decimal _topUp;
    public decimal TopUp
    {
        get => _topUp;
        set
        {
            if (_topUp != value) 
            {
                _topUp = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand OpenSavingCommand { get; set; }
    public ICommand CloseSavingCommand { get; set; }
    public ICommand OpenSavingEditorCommand { get; set; }
    public ICommand CloseSavingEditorCommand { get; set; }
    public ICommand OpenTopUpEditorCommand { get; set; }
    public ICommand CloseTopUpEditorCommand { get; set; }
    public ICommand CreateSavingCommand { get; set; }
    public ICommand GetSavingsPageCommand { get; set; }
    public ICommand UpdateSavingCommand { get; set; }
    public ICommand DeleteSavingCommand { get; set; }

    public SavingsViewModel(UserSession userSession, SavingsService savingsService)
    {
        _userSession = userSession;
        _savingsService = savingsService;

        IsTopUpEditorVisible = false;
        IsSavingVisible = false;

        SelectedSaving = new SavingModel();
        NewSaving = new SavingModel();

        OpenSavingCommand = new AsyncRelayCommand(OpenSavingAsync);
        CloseSavingCommand = new AsyncRelayCommand(CloseSavingAsync);
        OpenSavingEditorCommand = new AsyncRelayCommand(OpenSavingEditorAsync);
        CloseSavingEditorCommand = new AsyncRelayCommand(CloseSavingEditorAsync);
        OpenTopUpEditorCommand = new AsyncRelayCommand(OpenTopUpEditorAsync);
        CloseTopUpEditorCommand = new AsyncRelayCommand(CloseTopUpEditorAsync);
        CreateSavingCommand = new AsyncRelayCommand(CreateSavingAsync);
        GetSavingsPageCommand = new AsyncRelayCommand(GetSavingsPageAsync);
        UpdateSavingCommand = new AsyncRelayCommand(UpdateSavingAsync);
        DeleteSavingCommand = new AsyncRelayCommand(DeleteSavingAsync);        
    }

    private async Task OpenSavingAsync(object parameter)
    {
        IsSavingVisible = true;
    }

    private async Task CloseSavingAsync(object parameter)
    {
        IsSavingVisible = false;
        SelectedSaving = null;
    }

    private async Task OpenSavingEditorAsync(object parameter)
    {
        IsSavingEditorVisible = true;
        IsSavingVisible = false;
    }

    private async Task CloseSavingEditorAsync(object parameter)
    {
        IsSavingEditorVisible = false; 
        IsSavingVisible = true;
        NewSaving = null;
    }

    private async Task OpenTopUpEditorAsync(object parameter)
    {
        IsTopUpEditorVisible = true;
        IsSavingVisible = false;
    }

    private async Task CloseTopUpEditorAsync(object parameter)
    {
        IsTopUpEditorVisible = false;
        IsSavingVisible = true;
        TopUp = 0;
    }

    private async Task CreateSavingAsync(object parameter)
    {
        await _savingsService.CreateSavingAsync(NewSaving);
    }

    private async Task GetSavingsPageAsync(object parameter)
    {
        await _savingsService.GetSavingsPageAsync();
    }

    private async Task UpdateSavingAsync(object parameter)
    {
        await _savingsService.UpdateSavingAsync(new BackendApi.SavingTopUpDTO { SavingId = SelectedSaving.Id, TopUpAmount = TopUp });
    }

    private async Task DeleteSavingAsync(object parameter)
    {
        await _savingsService.DeleteSavingAsync(SelectedSaving);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
