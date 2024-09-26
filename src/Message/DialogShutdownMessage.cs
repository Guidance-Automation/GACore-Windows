namespace GACore.UI.Message;

public class DialogShutdownMessage<T>(T dialogSource) where T : Enum
{
    public T DialogSource { get; } = dialogSource;
}