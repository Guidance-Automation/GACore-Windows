using NLog;
using System.Windows.Input;

namespace GACore.UI.Command;

public class CustomCommandAsync(Func<object?, Task> execute, Func<bool>? canExecute = null) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private bool _isExecuting;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public bool CanExecute()
    {
        return !_isExecuting && (canExecute?.Invoke() ?? true);
    }

    public async Task ExecuteAsync(object? parameter = null)
    {
        if (CanExecute())
        {
            try
            {
                _isExecuting = true;
                await execute(parameter);
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
        ExecuteAsync(parameter).FireAndForgetSafeAsync(_logger);
    }
}