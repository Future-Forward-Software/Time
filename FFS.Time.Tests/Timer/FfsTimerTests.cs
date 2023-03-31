using FFS.Time.Timer;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FFS.Time.Tests.Timer
{
    public class FfsTimerTests
    {
        private int _numberOfExecutions = 0;

        // ironically the reason this implementation exists is because these tests are hard to perform (so abstract the timer)
        // as you add more tests to a project there is overhead that causes some time to be lost in the timer execution
        // and so as the time between timer actions goes down, the expected amount of timer actions invoked gets less stable
        // that stability is improved by increasing time between actions which is why these tests are slow
        [Theory]
        [InlineData(10000, 1)]
        [InlineData(10000, 5)]
        [InlineData(10000, 10)]
        [InlineData(20000, 2)]
        public async Task Start_ExecutesScheduledEvent(int msBetweenExecutions, int numberOfExecutions)
        {
            var timer = new FfsTimer();
            timer.Elapsed += ActionToPerform;

            timer.Start(msBetweenExecutions);
            for (int i = 0; i < numberOfExecutions; i++)
            {
                await Task.Delay(msBetweenExecutions);
            }
            timer.Stop();

            _numberOfExecutions.Should().Be(numberOfExecutions);
        }

        [Fact]
        public void StartNow_ExecutesEventImmediately()
        {
            var timer = new FfsTimer();
            timer.Elapsed += ActionToPerform;

            timer.StartNow(100000);
            timer.Stop();
            _numberOfExecutions.Should().Be(1);
        }

        [Fact]
        public async Task StartNow_ExecutesFurtherEvents()
        {
            var timer = new FfsTimer();
            timer.Elapsed += ActionToPerform;

            timer.StartNow(1000);
            await Task.Delay(1000);
            timer.Stop();

            _numberOfExecutions.Should().Be(2);
        }

        private void ActionToPerform()
        {
            Interlocked.Increment(ref _numberOfExecutions);
        }
    }
}
