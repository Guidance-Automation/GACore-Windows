using System.Windows.Input;

namespace GACore.UI.Command;

public class CustomCommandAsync(Func<object?, Task> executeAsync, Predicate<object?> canExecute) : ICommand
{
    private readonly Func<object?, Task> _execute = executeAsync;
    private readonly Predicate<object?> _canExecute = canExecute;
    private bool _isExecuting;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
        return !_isExecuting && (_canExecute == null || _canExecute(parameter));
    }

    public async void Execute(object? parameter)
    {
        _isExecuting = true;
        await _execute(parameter);
        _isExecuting = false;
    }
}