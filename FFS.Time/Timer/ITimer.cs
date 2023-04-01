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
        void RunNowAndStart(double ms);

        /// <summary>
        /// Start but wait for the incremented time before the first event will run
        /// </summary>
        /// <param name="ms"></param>
        void Start(double ms);

        /// <summary>
        /// Start in N ms and then run on a duration thereafter. Useful when you have a service that must run at eg 1am every night. Deploy at 12am and it should run in 1 hour and then every 24 hours.
        /// </summary>
        /// <param name="runIn"></param>
        /// <param name="ms"></param>
        void RunInAndStart(double runIn, double ms);

        void Stop();
    }
}
