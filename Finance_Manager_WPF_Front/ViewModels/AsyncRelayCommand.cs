using System.Windows.Input;

namespace Finance_Manager_WPF_Front.ViewModels;

public class AsyncRelayCommand : ICommand
{
    private readonly Func<object, Task> _execute;
    private readonly Func<object, bool> _canExecute;
    private bool _isExecuting;
    public AsyncRelayCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
    }

    public async void Execute(object parameter)
    {
        _isExecuting = true;
        RaiseCanExecuteChanged();

        try
        {
            await _execute(parameter);
        }
        catch (Exception ex) 
        {
            Serilog.Log.Error(ex, "Error in command.");
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
