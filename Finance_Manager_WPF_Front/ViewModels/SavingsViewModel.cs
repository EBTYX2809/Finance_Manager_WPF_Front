﻿using Finance_Manager_WPF_Front.Models;
using Finance_Manager_WPF_Front.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class SavingsViewModel : INotifyPropertyChanged
{
    public UserSession _userSession { get; set; }
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

    private decimal? _sumSavings;
    public decimal? SumSavings
    {
        get => _sumSavings;
        set
        {
            if (_sumSavings != value)
            {
                _sumSavings = value;
                OnPropertyChanged();
            }
        }
    }

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

        NewSaving = new SavingModel();

        OpenSavingEditorCommand = new AsyncRelayCommand(OpenSavingEditorAsync);
        CloseSavingEditorCommand = new AsyncRelayCommand(CloseSavingEditorAsync);
        OpenTopUpEditorCommand = new AsyncRelayCommand(OpenTopUpEditorAsync);
        CloseTopUpEditorCommand = new AsyncRelayCommand(CloseTopUpEditorAsync);
        CreateSavingCommand = new AsyncRelayCommand(CreateSavingAsync);
        GetSavingsPageCommand = new AsyncRelayCommand(GetSavingsPageAsync);
        UpdateSavingCommand = new AsyncRelayCommand(UpdateSavingAsync);
        DeleteSavingCommand = new AsyncRelayCommand(DeleteSavingAsync);        
    }

    private async Task OpenSavingEditorAsync(object parameter)
    {
        IsSavingEditorVisible = true;        
    }

    private async Task CloseSavingEditorAsync(object parameter)
    {
        IsSavingEditorVisible = false; 
        NewSaving = null;
    }

    private async Task OpenTopUpEditorAsync(object parameter)
    {
        IsTopUpEditorVisible = true;   
        if(parameter is SavingModel savingModel)
        {
            NewSaving = savingModel;
        }
    }

    private async Task CloseTopUpEditorAsync(object parameter)
    {
        IsTopUpEditorVisible = false;
        NewSaving = null;
        TopUp = 0;
    }

    private async Task CreateSavingAsync(object parameter)
    {
        await _savingsService.CreateSavingAsync(NewSaving);
        UpdateSumSavings();
        CloseSavingEditorAsync(null);
    }

    private async Task GetSavingsPageAsync(object parameter)
    {
        await _savingsService.GetSavingsPageAsync();
        UpdateSumSavings();
    }

    private async Task UpdateSavingAsync(object parameter)
    {
        await _savingsService.UpdateSavingAsync(new BackendApi.SavingTopUpDTO { SavingId = NewSaving.Id, TopUpAmount = TopUp });
        UpdateSumSavings();
        CloseTopUpEditorAsync(null);
    }

    private async Task DeleteSavingAsync(object parameter)
    {
        if (parameter is SavingModel savingModel)
            await _savingsService.DeleteSavingAsync(savingModel);
        UpdateSumSavings();
    }

    public void UpdateSumSavings()
    {
        SumSavings = _userSession.CurrentUser.Savings.Sum(s => s.CurrentAmount);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
