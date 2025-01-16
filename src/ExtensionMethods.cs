using NLog;

namespace GACore.UI;

internal static class ExtensionMethods
{
    internal static async void FireAndForgetSafeAsync(this Task task, ILogger? logger = null)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            logger?.Error($"Exception raised in async command { ex.Message}");
        }
    }
}