using System;

namespace FFS.Time.TimeManager
{
    internal class FfsTimeManager : ITime
    {
        public DateTime Now => DateTime.Now;

        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

        public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    }
}
