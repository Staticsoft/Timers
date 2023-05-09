using Staticsoft.Timers.Abstractions;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Staticsoft.Timers.Memory;

public class NextMinuteTimer : NextMinute
{
    const int OneMinute = 60_000;

    readonly Timer EachMinuteTimer = new Timer(OneMinute);
    readonly Timer FirstMinuteTimer;
    readonly ControlledNextMinute Controlled;

    public NextMinuteTimer(ControlledNextMinute controlled)
    {
        Controlled = controlled;
        var now = DateTime.Now;
        var nextMinute = now.AddMinutes(1).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
        var timeUntilNextMinute = nextMinute - now;
        FirstMinuteTimer = new Timer(timeUntilNextMinute.TotalMilliseconds);
        FirstMinuteTimer.Elapsed += TickFirstMinute;
        EachMinuteTimer.Elapsed += TickEachMinute;
        FirstMinuteTimer.Start();
    }

    public Task Wait()
        => Controlled.Wait();

    void TickFirstMinute(object sender, ElapsedEventArgs e)
    {
        EachMinuteTimer.Start();
        FirstMinuteTimer.Stop();
        FirstMinuteTimer.Elapsed -= TickFirstMinute;
        Controlled.Tick();
    }

    void TickEachMinute(object sender, ElapsedEventArgs e)
        => Controlled.Tick();
}
