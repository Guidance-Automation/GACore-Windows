﻿using GAAPICommon.Enums;
using GAAPICommon.Messages;
using System.Net;

namespace GACore.DemoApp;

/// <summary>
/// Just makes a random kingpin state
/// </summary>
public static class FooKingpinState
{
    public static KingpinStateDto GetKingpinState()
    {
        return new KingpinStateDto()
        {
            PositionControlStatus = Tools.RandomEnumValue<PositionControlStatus>(),
            NavigationStatus = Tools.RandomEnumValue<NavigationStatus>(),
            DynamicLimiterStatus = Tools.RandomEnumValue<DynamicLimiterStatus>(),
            Alias = "Foo Kingpin",
            IsVirtual = Tools.Random.Next(0, 2) > 0,
            Tick = 0,
            X = Tools.Random.Next(-10, 10),
            Y = Tools.Random.Next(-10, 10),
            Heading = Tools.Random.Next(-3, 3),
            CurrentMovementType = Tools.RandomEnumValue<MovementType>(),
            IPAddress = IPAddress.Loopback.ToString(),
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.NoFault,
            FrozenState = Tools.RandomEnumValue<FrozenState>()
        };
    }

    public static KingpinStateDto FromGood()
    {
        KingpinStateDto state = GetKingpinState();
        state.PositionControlStatus = PositionControlStatus.Okposition;
        state.NavigationStatus = NavigationStatus.Oknavigation;
        state.DynamicLimiterStatus = DynamicLimiterStatus.Ok;
        return state;
    }
}