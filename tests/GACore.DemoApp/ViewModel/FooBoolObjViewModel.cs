using GACore.DemoApp.Model;
using GACore.UI.Command;
using GACore.UI.ViewModel;
using System.Runtime.Versioning;
using System.Windows.Input;

namespace GACore.DemoApp.ViewModel;

[SupportedOSPlatform("windows")]
public class FooBoolObjViewModel : AbstractViewModel<FooBoolObj>
{
	public bool IsSet
	{
		get { return Model != null && Model.IsSet; }
		private set
		{
            Model?.ToggleIsSet();
            OnNotifyPropertyChanged();
		}
	}

    public bool IsSetAsync
    {
        get { return Model != null && Model.IsSetAsync; }
        private set
        {
            Model?.ToggleIsSetAsync();
            OnNotifyPropertyChanged();
        }
    }

    protected override void HandleModelUpdate(FooBoolObj? oldValue, FooBoolObj? newValue)
	{
		OnNotifyPropertyChanged(nameof(IsSet));
		base.HandleModelUpdate(oldValue, newValue);
	}

	public ICommand? ClickCommand { get; set; }
    public ICommand? AsyncCommand { get; set; }

    private void HandleCommands()
	{
		ClickCommand = new CustomCommand(ClickCommandClick, CanClickCommandClick);
		AsyncCommand = new CustomCommandAsync(ClickCommandAsync, CanClickCommandAsync);

    }

    private void ClickCommandClick(object? obj)
    {
        IsSet = false; // Forces recalc
    }

    private async Task ClickCommandAsync(object? obj)
    {
        await Task.Delay(100);
        IsSetAsync = false; // Forces recalc
    }

    private bool CanClickCommandClick(object? obj)
    {
        return true;
    }

    private bool CanClickCommandAsync()
    {
        return true;
    }

    public FooBoolObjViewModel()
	{
		HandleCommands();
	}
}
