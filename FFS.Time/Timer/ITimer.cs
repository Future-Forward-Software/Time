using System;
using System.Timers;

namespace FFS.Time.Timer
{
    public interface ITimer : IDisposable
    {
        event ElapsedEventHandler Elapsed;
        void Start(int ms);
        void Stop();
    }
}
