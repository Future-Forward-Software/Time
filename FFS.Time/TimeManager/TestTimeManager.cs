using System;

namespace FFS.Time.TimeManager
{
    public class TestTimeManager : ITime
    {
        private DateTimeOffset? nowUtc;

        public void Override(DateTimeOffset overrideValue) => nowUtc = overrideValue;

        public DateTime Now => nowUtc?.LocalDateTime ?? DateTime.Now;

        public DateOnly Today => DateOnly.FromDateTime(Now);
    }
}
