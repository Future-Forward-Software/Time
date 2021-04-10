using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using FFS.Time.Timer;
using FluentAssertions;
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
        [InlineData(1000, 1)]
        [InlineData(1000, 5)]
        [InlineData(1000, 10)]
        [InlineData(2000, 2)]
        public async Task Timer_ExecutesScheduledEvent(int msBetweenExecutions, int numberOfExecutions)
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

        private void ActionToPerform(object sender, ElapsedEventArgs e)
        {
            Interlocked.Increment(ref _numberOfExecutions);
        }
    }
}
