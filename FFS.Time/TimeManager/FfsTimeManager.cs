using System;

namespace FFS.Time.TimeManager
{
    internal class FfsTimeManager : ITime
    {
        public DateTime Now => DateTime.Now;

        public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    }
}
