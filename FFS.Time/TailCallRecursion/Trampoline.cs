using System;

namespace FFS.Time.TailCallRecursion
{
    internal static class Trampoline
    {
        public static T Execute<T>(Func<Bounce<T>> func)
        {
            do
            {
                var recursionResult = func();
                if (recursionResult.IsFinalResult)
                    return recursionResult.Result;
                func = recursionResult.NextBounce;
            } while (true);
        }

        public static Bounce<T> Result<T>(T result)
        {
            return new Bounce<T>(result, true, null);
        }

        public static Bounce<T> Bounce<T>(Func<Bounce<T>> nextStep)
        {
            return new Bounce<T>(default, false, nextStep);
        }
    }
}
