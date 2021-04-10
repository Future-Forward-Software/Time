using System;

namespace FFS.Time.TimeManager
{
    public interface ITimeManager
    {
        DateTime Now { get; }
        DateTime Today { get; }
        DateTime UtcNow { get; }
    }
}
