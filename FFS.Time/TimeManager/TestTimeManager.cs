using System;

namespace FFS.Time.TimeManager
{
    public class TestTimeManager : ITimeManager
    {
        private DateTime? _now;
        private DateTime? _today;
        private DateTime? _utcNow;

        public void OverrideNow(DateTime overrideValue) => _now = overrideValue;
        public void OverrideToday(DateTime overrideValue) => _today = overrideValue;
        public void OverrideUtcNow(DateTime overrideValue) => _utcNow = overrideValue;

        public DateTime Now => _now ?? DateTime.Now;

        public DateTime Today => _today ?? DateTime.Today;

        public DateTime UtcNow => _utcNow ?? DateTime.UtcNow;
    }
}
