using FFS.Time.TimeManager;
using FFS.Time.Timer;
using Microsoft.Extensions.DependencyInjection;

namespace FFS.Time
{
    public static class TimeExtensions
    {
        public static IServiceCollection AddTime(this IServiceCollection services)
        {
            services.AddSingleton<ITimeManager, FfsTimeManager>();
            services.AddSingleton<ITimer, FfsTimer>();

            return services;
        }
    }
}
