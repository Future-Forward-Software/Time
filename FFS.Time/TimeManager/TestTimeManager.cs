using System;

namespace FFS.Time.TimeManager
{
    public class TestTimeManager : ITime
    {
        private DateTimeOffset? utcNow;

        public void Override(DateTimeOffset overrideValue) => utcNow = overrideValue;

        public DateTime Now => utcNow?.LocalDateTime ?? DateTime.Now;

        public DateOnly Today => DateOnly.FromDateTime(Now);

        public DateTimeOffset UtcNow => utcNow ?? DateTimeOffset.UtcNow;
    }
}
