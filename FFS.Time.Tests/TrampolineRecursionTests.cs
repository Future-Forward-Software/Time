using System.Diagnostics;
using FFS.Time.TailCallRecursion;
using FluentAssertions;
using Xunit;

namespace FFS.Time.Tests
{
    public class TrampolineRecursionTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void NormalRecursion_CreatesStackFramePerRecursion(int recursions)
        {
            var stackTrace = new StackTrace();
            var startingFrameCount = stackTrace.FrameCount;

            var endingFrameCount = Recursive(recursions).frameCount;
            var amountOfFramesUsed = endingFrameCount - startingFrameCount;

            amountOfFramesUsed.Should().Be(recursions);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void TrampolineRecursion_CreatesThreeStackFrameForTrampolineExecuteAndTrampolineBounceAndTrampolineResult(int recursions)
        {
            var stackTrace = new StackTrace();
            var startingFrameCount = stackTrace.FrameCount;

            var endingFrameCount = Trampoline.Execute(() => TrampolineRecursive(recursions)).frameCount;
            var amountOfFramesUsed = endingFrameCount - startingFrameCount;

            amountOfFramesUsed.Should().Be(3);
        }

        private (int numberExecuted, int frameCount) Recursive(int numberOfRecursions, int numberExecuted = 0)
        {
            var stackTrace = new StackTrace();

            numberExecuted++;

            if (numberExecuted == numberOfRecursions)
                return (numberExecuted, stackTrace.FrameCount);
            else
                return Recursive(numberOfRecursions, numberExecuted);
        }

        private Bounce<(int numberExecuted, int frameCount)> TrampolineRecursive(int numberOfRecursions, int numberExecuted = 0)
        {
            var stackTrace = new StackTrace();

            numberExecuted++;

            if (numberExecuted == numberOfRecursions)
                return Trampoline.Result((numberExecuted, stackTrace.FrameCount));
            else
                return Trampoline.Bounce(() => TrampolineRecursive(numberOfRecursions, numberExecuted));
        }
    }
}
