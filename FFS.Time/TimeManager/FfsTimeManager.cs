using System;

namespace FFS.Time.TimeManager
{
    internal class FfsTimeManager : ITimeManager
    {
        public DateTime Now => DateTime.Now;

        public DateTime Today => DateTime.Today;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
