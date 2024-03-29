﻿using Microsoft.Extensions.DependencyInjection;
using Staticsoft.Timers.Abstractions;
using Staticsoft.Timers.Memory;
using System.Timers;
using Xunit;

namespace Staticsoft.Timers.Tests;

public class ControlledNextMinuteTests : NextMinuteTests
{
    protected override IServiceCollection Services => base.Services
        .AddSingleton<ControlledNextMinute>()
        .ReuseSingleton<NextMinute, ControlledNextMinute>();

    const int OneSecond = 1000;
    readonly Timer Timer = new Timer(OneSecond);

    ControlledNextMinute NextMinute
        => Get<ControlledNextMinute>();

    public ControlledNextMinuteTests()
    {
        Timer.Elapsed += TickNextMinute;
        Timer.Start();
    }

    [Fact]
    public void CanTickWithoutWaiting()
    {
        NextMinute.Tick();
    }

    void TickNextMinute(object sender, ElapsedEventArgs e)
        => NextMinute.Tick();
}
