using Staticsoft.Timers.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Staticsoft.Timers.Memory
{
    public class ControlledNextMinute : NextMinute
    {
        readonly SemaphoreSlim Semaphore = new SemaphoreSlim(0);
        int WaitingCount = 0;

        public async Task Wait()
        {
            Interlocked.Increment(ref WaitingCount);
            await Semaphore.WaitAsync();
        }

        public void Tick()
        {
            var waiting = Interlocked.Exchange(ref WaitingCount, 0);
            if (waiting != 0)
            {
                Semaphore.Release(waiting);
            }
        }
    }
}
