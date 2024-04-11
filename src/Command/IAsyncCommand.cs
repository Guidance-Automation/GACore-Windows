using System.Windows.Input;

namespace GACore.UI.Command;
public interface IAsyncCommand : ICommand
{
    public Task ExecuteAsync(object? parameter);
}
