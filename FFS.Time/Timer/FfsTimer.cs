using System.Timers;
using NetTimer = System.Timers.Timer;

namespace FFS.Time.Timer
{
    internal class FfsTimer : ITimer
    {
        private NetTimer _timer = new NetTimer();

        public event ElapsedEventHandler Elapsed {
            add => _timer.Elapsed += value;
            remove => _timer.Elapsed -= value;
        }

        public void Start(double ms)
        {
            _timer.Interval = ms;
            _timer.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
        }

        public void Dispose()
        {
            Stop();
            _timer.Dispose();
        }
    }
}
