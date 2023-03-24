using FFS.Time.TimeManager;
using FFS.Time.Timer;
using Microsoft.Extensions.DependencyInjection;

namespace FFS.Time
{
    public static class TimeExtensions
    {
        public static IServiceCollection AddTime(this IServiceCollection services)
        {
            services.AddSingleton<ITime, FfsTimeManager>();
            services.AddSingleton<ITimerFactory, TimerFactory>();

            return services;
        }
    }
}
