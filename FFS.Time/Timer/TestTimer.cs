﻿using System;
using System.Threading;

namespace FFS.Time.Timer
{
    public sealed class TestTimer : ITimer
    {
        private double _msElapsedTime = 0;
        private int _totalAmountOfExecutionsPerformed = 0;
        private double _msToWaitBetweenWork;
        private bool _runsFirstOnDifferentInterval;

        public event ElapsedHandler Elapsed;

        public void Start(double ms)
        {
            _msToWaitBetweenWork = ms;
        }

        public void RunNowAndStart(double ms)
        {
            Elapsed.Invoke();
            _msToWaitBetweenWork = ms;
        }

        public void RunInAndStart(double runIn, double ms)
        {
            _msToWaitBetweenWork = runIn;
            _runsFirstOnDifferentInterval = true;

            void ResetTimer()
            {
                _msToWaitBetweenWork = ms;
                _totalAmountOfExecutionsPerformed = 0;

                Elapsed -= ResetTimer;
            }

            Elapsed += ResetTimer;
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

            if (!double.IsInfinity(executionsThatShouldBePerformed) && executionsNotYetPerformed > 0)
                return true;

            return false;
        }

        private void ExecuteEvent()
        {
            Elapsed.Invoke();

            if(!_runsFirstOnDifferentInterval)
            {
                Interlocked.Increment(ref _totalAmountOfExecutionsPerformed);
            }

            _runsFirstOnDifferentInterval = false;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
