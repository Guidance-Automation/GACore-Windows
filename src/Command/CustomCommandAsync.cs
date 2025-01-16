using NLog;
using System.Windows.Input;

namespace GACore.UI.Command;

public class CustomCommandAsync(Func<Task> execute, Func<bool>? canExecute = null) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private bool _isExecuting;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public bool CanExecute()
    {
        return !_isExecuting && (canExecute?.Invoke() ?? true);
    }

    public async Task ExecuteAsync()
    {
        if (CanExecute())
        {
            try
            {
                _isExecuting = true;
                await execute();
            }
            finally
            {
                _isExecuting = false;
            }
        }

        RaiseCanExecuteChanged();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter)
    {
        return CanExecute();
    }

    public void Execute(object? parameter)
    {
        ExecuteAsync().FireAndForgetSafeAsync(_logger);
    }
}