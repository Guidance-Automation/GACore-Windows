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
        Func<object?, Task> execute = A.Fake<Func<object?, Task>>();
        CustomCommandAsync command = new(execute);
        bool canExecute = command.CanExecute();

        Assert.That(canExecute);
    }

    [Test]
    public void CanExecute_UsesProvidedCanExecuteDelegate()
    {
        Func<object?, Task> execute = A.Fake<Func<object?, Task>>();
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
        Func<object?, Task> execute = A.Fake<Func<object?, Task>>();
        A.CallTo(() => execute(null)).Returns(Task.CompletedTask);
        CustomCommandAsync command = new(execute);
        await command.ExecuteAsync();

        A.CallTo(() => execute(null)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ExecuteAsync_RaisesCanExecuteChanged_AfterExecution()
    {
        Func<object?, Task> execute = A.Fake<Func<object?, Task>>();
        A.CallTo(() => execute(null)).Returns(Task.CompletedTask);
        CustomCommandAsync command = new(execute);
        bool canExecuteChangedRaised = false;
        command.CanExecuteChanged += (sender, args) => canExecuteChangedRaised = true;
        command.ExecuteAsync().FireAndForgetSafeAsync();

        Assert.That(canExecuteChangedRaised);
    }

    [Test]
    public void ICommand_CanExecute_CallsUnderlyingCanExecute()
    {
        Func<object?, Task> execute = A.Fake<Func<object?, Task>>();
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
        Func<object?, Task> execute = A.Fake<Func<object?, Task>>();
        A.CallTo(() => execute(null)).Returns(Task.CompletedTask);
        CustomCommandAsync command = new(execute);
        ((ICommand)command).Execute(null);

        A.CallTo(() => execute(null)).MustHaveHappenedOnceExactly();
    }
}