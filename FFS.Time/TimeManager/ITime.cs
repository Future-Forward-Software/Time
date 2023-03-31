using System;

namespace FFS.Time.TimeManager
{
    public interface ITime
    {
        DateTime Now { get; }
        DateTimeOffset UtcNow { get; }

        DateOnly Today { get; }
    }
}
