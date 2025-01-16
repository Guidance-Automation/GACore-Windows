namespace GACore.DemoApp.Model;

public class FooBoolObj
{
    public bool IsSet { get; set; } = false;

    public bool IsSetAsync { get; set; } = false;

    public FooBoolObj()
    {
    }

    public void ToggleIsSet()
    {
        bool current = IsSet;
        IsSet = !current;
    }

    public void ToggleIsSetAsync()
    {
        bool current = IsSetAsync;
        IsSetAsync = !current;
    }
}