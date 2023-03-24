using System;

namespace FFS.Time.TimeManager
{
    public interface ITime
    {
        DateTime Now { get; }
        DateOnly Today { get; }
    }
}
