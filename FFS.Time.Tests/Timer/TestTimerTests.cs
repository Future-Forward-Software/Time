using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using FFS.Time.Timer;
using FluentAssertions;
using Xunit;

namespace FFS.Time.Tests.Timer
{
    public class TestTimerTests
    {
        private int _numberOfExecutions = 0;

        [Fact]
        public void SimulatePassageOfTime_WaitExactlyForOneEvent_ExecutesOneEvent()
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.Start(1000);
            timer.SimulateTime(1000);
            timer.Stop();

            _numberOfExecutions.Should().Be(1);
        }

        [Fact]
        public void SimulatePassageOfTime_WaitLessThanOneEvent_DoesNotExecuteEvent()
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.Start(1000);
            timer.SimulateTime(999);
            timer.Stop();

            _numberOfExecutions.Should().Be(0);
        }

        [Theory]
        [InlineData(1000, 1001)]
        [InlineData(1000, 1100)]
        [InlineData(1000, 1200)]
        [InlineData(1000, 1300)]
        [InlineData(1000, 1400)]
        [InlineData(1000, 1500)]
        [InlineData(1000, 1600)]
        [InlineData(1000, 1700)]
        [InlineData(1000, 1800)]
        [InlineData(1000, 1900)]
        [InlineData(1000, 1999)]
        public void SimulatePassageOfTime_WaitMoreThanOneEventButLessThanTwo_ExecutesOnesEvent(int timeBetweenEvents, int timeToWait)
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.Start(timeBetweenEvents);
            timer.SimulateTime(timeToWait);
            timer.Stop();

            _numberOfExecutions.Should().Be(1);
        }

        [Theory]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.OneSecond, 1)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.TwoSeconds, 2)]
        [InlineData(TimeIntervals.OneSecond, 2500, 2)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.ThreeSeconds, 3)]
        [InlineData(TimeIntervals.OneSecond, 3500, 3)]
        [InlineData(TimeIntervals.OneSecond, 4000, 4)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.FiveSeconds, 5)]
        [InlineData(TimeIntervals.OneSecond, 5500, 5)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.OneMinute, 60)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.FiveMinutes, 300)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.TenMinutes, 600)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.ThirtyMinutes, 1800)]
        [InlineData(TimeIntervals.OneSecond, TimeIntervals.OneHour, 3600)]
        [InlineData(TimeIntervals.FiveMinutes, TimeIntervals.OneHour, 12)]
        [InlineData(TimeIntervals.ThirtyMinutes, TimeIntervals.OneHour, 2)]
        public void SimulatePassageOfTime_Wait_ExecutesCorrectAmountOfEvents(int timeBetweenEvents, int timeToWait, int expectedAmountOfExecuted)
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.Start(timeBetweenEvents);
            timer.SimulateTime(timeToWait);
            timer.Stop();

            _numberOfExecutions.Should().Be(expectedAmountOfExecuted);
        }

        [Fact]
        public void RunNowAndStart_ExecutesEventImmediately()
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.RunNowAndStart(100000);
            timer.Stop();
            _numberOfExecutions.Should().Be(1);
        }

        [Fact]
        public void RunNowAndStart_ExecutesFurtherEvents()
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.RunNowAndStart(1000);
            timer.SimulateTime(1000);
            timer.Stop();

            _numberOfExecutions.Should().Be(2);
        }

        [Fact]
        public void RunInAndStart_ExecutesFirstRunInGivenTimeAndThenAllOtherRunsInOtherTime()
        {
            var timer = new TestTimer();
            timer.Elapsed += ActionToPerform;

            timer.RunInAndStart(1000, 5000);
            timer.SimulateTime(6000);
            timer.Stop();

            _numberOfExecutions.Should().Be(2);
        }

        private void ActionToPerform()
        {
            Interlocked.Increment(ref _numberOfExecutions);
        }
    }
}
