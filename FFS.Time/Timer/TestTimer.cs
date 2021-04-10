using System;
using System.Threading;
using System.Timers;

namespace FFS.Time.Timer
{
    public class TestTimer : ITimer
    {
        private double _msElapsedTime = 0;
        private int _totalAmountOfExecutionsPerformed = 0;
        private double _msToWaitBetweenWork;

        public event ElapsedEventHandler Elapsed;

        public void Start(double ms)
        {
            _msToWaitBetweenWork = ms;
        }

        public void Stop()
        {

        }

        public void SimulateTime(int ms)
        {
            Interlocked.Exchange(ref _msElapsedTime, _msElapsedTime + ms);

            while (ShouldExecuteEvent())
            {
                ExecuteEvent();
            }
        }

        private bool ShouldExecuteEvent()
        {
            var executionsThatShouldBePerformed = Math.Floor(_msElapsedTime / _msToWaitBetweenWork);
            var executionsNotYetPerformed = executionsThatShouldBePerformed - _totalAmountOfExecutionsPerformed;

            if (executionsNotYetPerformed > 0)
                return true;

            return false;
        }

        private void ExecuteEvent()
        {
            Elapsed.Invoke(this, null);
            Interlocked.Increment(ref _totalAmountOfExecutionsPerformed);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
