using Microsoft.Extensions.DependencyInjection;
using Staticsoft.Testing;
using Staticsoft.Timers.Abstractions;
using Staticsoft.Timers.Memory;

namespace Staticsoft.Timers.Tests
{
    public class NextMinuteTimerTests : NextMinuteTests<NextMinuteTimerServices> { }

    public class NextMinuteTimerServices : UnitServicesBase
    {
        protected override IServiceCollection Services => base.Services
            .AddSingleton<ControlledNextMinute>()
            .AddSingleton<NextMinute, NextMinuteTimer>();
    }
}
