using Microsoft.Extensions.DependencyInjection;
using Staticsoft.Timers.Abstractions;
using Staticsoft.Timers.Memory;

namespace Staticsoft.Timers.Tests;

public class NextMinuteTimerTests : NextMinuteTests
{
    protected override IServiceCollection Services => base.Services
        .AddSingleton<ControlledNextMinute>()
        .AddSingleton<NextMinute, NextMinuteTimer>();
}
