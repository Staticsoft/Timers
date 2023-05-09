using Staticsoft.Testing;
using Staticsoft.Timers.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Staticsoft.Timers.Tests;

public abstract class NextMinuteTests : TestBase<NextMinute>
{
    const int TwoMinutes = OneMinute * 2;
    const int OneMinute = OneSecond * 60;
    const int OneSecond = 1000;

    [Fact(Timeout = OneMinute + OneSecond)]
    public async Task WaitsForNextMinute()
    {
        var waitingMoment = DateTime.Now;
        await SUT.Wait();
        var elapsed = DateTime.Now - waitingMoment;
        Assert.InRange(elapsed, TimeSpan.Zero, TimeSpan.FromMilliseconds(OneMinute + OneSecond));
    }

    [Fact(Timeout = OneMinute + OneSecond)]
    public async Task WaitsForNextMinuteConcurrently()
    {
        var waitingMoment = DateTime.Now;
        await Task.WhenAll(SUT.Wait(), SUT.Wait());
        var elapsed = DateTime.Now - waitingMoment;
        Assert.InRange(elapsed, TimeSpan.Zero, TimeSpan.FromMilliseconds(OneMinute + OneSecond));
    }

    [Fact(Timeout = TwoMinutes + OneSecond)]
    public async Task WaitsForTwoMinutes()
    {
        var waitingMoment = DateTime.Now;
        await SUT.Wait();
        await SUT.Wait();
        var elapsed = DateTime.Now - waitingMoment;
        Assert.InRange(elapsed, TimeSpan.Zero, TimeSpan.FromMilliseconds(TwoMinutes + OneSecond));
    }
}
