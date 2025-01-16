using FakeItEasy;
using GACore.UI.Command;
using System.Windows.Input;

namespace GACore.UI.Test;

[TestFixture]
public class TCustomCommandAsync
{
    [Test]
    public void CanExecute_ReturnsTrue_WhenNotExecuting_AndCanExecuteIsNull()
    {
        Func<Task> execute = A.Fake<Func<Task>>();
        CustomCommandAsync command = new(execute);
        bool canExecute = command.CanExecute();

        Assert.That(canExecute);
    }

    [Test]
    public void CanExecute_UsesProvidedCanExecuteDelegate()
    {
        Func<Task> execute = A.Fake<Func<Task>>();
        Func<bool> canExecute = A.Fake<Func<bool>>();
        A.CallTo(() => canExecute()).Returns(false);
        CustomCommandAsync command = new(execute, canExecute);
        bool result = command.CanExecute();

        Assert.That(result, Is.False);
        A.CallTo(() => canExecute()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task ExecuteAsync_CallsExecuteAndHandlesIsExecuting()
    {
        Func<Task> execute = A.Fake<Func<Task>>();
        A.CallTo(() => execute()).Returns(Task.CompletedTask);
        CustomCommandAsync command = new(execute);
        await command.ExecuteAsync();

        A.CallTo(() => execute()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ExecuteAsync_RaisesCanExecuteChanged_AfterExecution()
    {
        Func<Task> execute = A.Fake<Func<Task>>();
        A.CallTo(() => execute()).Returns(Task.CompletedTask);
        CustomCommandAsync command = new(execute);
        bool canExecuteChangedRaised = false;
        command.CanExecuteChanged += (sender, args) => canExecuteChangedRaised = true;
        command.ExecuteAsync().FireAndForgetSafeAsync();

        Assert.That(canExecuteChangedRaised);
    }

    [Test]
    public void ICommand_CanExecute_CallsUnderlyingCanExecute()
    {
        Func<Task> execute = A.Fake<Func<Task>>();
        Func<bool> canExecute = A.Fake<Func<bool>>();
        A.CallTo(() => canExecute()).Returns(true);
        CustomCommandAsync command = new(execute, canExecute);
        bool result = ((ICommand)command).CanExecute(null);

        Assert.That(result);
        A.CallTo(() => canExecute()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ICommand_Execute_CallsExecuteAsync()
    {
        Func<Task> execute = A.Fake<Func<Task>>();
        A.CallTo(() => execute()).Returns(Task.CompletedTask);
        CustomCommandAsync command = new(execute);
        ((ICommand)command).Execute(null);

        A.CallTo(() => execute()).MustHaveHappenedOnceExactly();
    }
}