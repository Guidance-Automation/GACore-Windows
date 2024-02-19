using GAAPICommon.Enums;
using System.Drawing;

namespace GACore.UI.Controls;

public static class BrushDictionaries
{
    public static Dictionary<PositionControlStatus, BrushCollection> PositionControlStatusBackgroundBrushCollectionDictionary { get; } = new Dictionary<PositionControlStatus, BrushCollection>
    {
        {PositionControlStatus.Okposition, new BrushCollection("OK", Color.Black, Color.Green) },
        {PositionControlStatus.Disabled, new BrushCollection("Disabled", Color.Silver, Color.Black) },
        {PositionControlStatus.Disabling, new BrushCollection("Disabling", Color.Silver, Color.Black) },
        {PositionControlStatus.NoWaypoints, new BrushCollection("No Waypoints", Color.Silver, Color.Yellow) },
        {PositionControlStatus.OutOfPosition, new BrushCollection("Out Of Position", Color.Black, Color.Orange) },
        {PositionControlStatus.WaypointDiscontinuity, new BrushCollection("Waypoint Discontinuity", Color.Black, Color.Crimson) }
    };
    public static Dictionary<DynamicLimiterStatus, BrushCollection> DynamicLimiterStatusBrushCollectionDictionary { get; } = new Dictionary<DynamicLimiterStatus, BrushCollection>
    {
        { DynamicLimiterStatus.Ok, new BrushCollection("OK", Color.Black, Color.Green) },
        { DynamicLimiterStatus.SafetySensor, new BrushCollection("Safety Sensor", Color.Black, Color.Yellow) },
        { DynamicLimiterStatus.Warning1, new BrushCollection("Warning 1", Color.Black, Color.Yellow) },
        { DynamicLimiterStatus.Warning2, new BrushCollection("Warning 2", Color.Black, Color.Yellow) },
        { DynamicLimiterStatus.MotorFault, new BrushCollection("Motor Fault", Color.Black, Color.Crimson) },
        { DynamicLimiterStatus.FastStop, new BrushCollection("Fast Stop", Color.Black, Color.Yellow) },
        { DynamicLimiterStatus.GoSlow, new BrushCollection("Go Slow", Color.Black, Color.Yellow) }
    };
    public static Dictionary<NavigationStatus, BrushCollection> NavigationStatusBackgroundBrushCollectionDictionary { get; } = new Dictionary<NavigationStatus, BrushCollection>
    {
        { NavigationStatus.Oknavigation, new BrushCollection("OK", Color.Black, Color.Green) },
        { NavigationStatus.Lost, new BrushCollection("Lost", Color.Black, Color.Crimson) },
        { NavigationStatus.AssociationFailure, new BrushCollection("Association Failure", Color.Black, Color.Crimson) },
        { NavigationStatus.HighUncertainty, new BrushCollection("High Uncertainty", Color.Black, Color.Orange) },
        { NavigationStatus.PoorAssociaton, new BrushCollection("Poor Association", Color.Black, Color.Yellow) },
        { NavigationStatus.NoResponse, new BrushCollection("No Response", Color.Black, Color.Crimson) },
        { NavigationStatus.NoScannerData, new BrushCollection("No Scanner Data", Color.Black, Color.Crimson) }
    };
}