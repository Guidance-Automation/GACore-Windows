using System.Windows.Media;

namespace GACore.UI.Controls.Extensions;
public static class Color_ExtensionMethods
{
    /// <summary>
    /// Converts the cross platform System.Drawing.Color to the WPF color.
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static Color ToWindowsColor(this System.Drawing.Color color)
    {
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}