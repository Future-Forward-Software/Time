using System;

namespace FFS.Time.Timer
{
    public delegate void ElapsedHandler();

    public interface ITimer : IDisposable
    {
        event ElapsedHandler Elapsed;
        /// <summary>
        /// Start now and then run the event again every increment
        /// </summary>
        /// <param name="ms"></param>
        void StartNow(double ms);
        /// <summary>
        /// Start but wait for the incremented time before the first event will run
        /// </summary>
        /// <param name="ms"></param>
        void Start(double ms);
        void Stop();
    }
}
