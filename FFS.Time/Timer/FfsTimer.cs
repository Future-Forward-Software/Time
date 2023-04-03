using System.Collections.Generic;
using System.Timers;
using NetTimer = System.Timers.Timer;

namespace FFS.Time.Timer
{
    internal class FfsTimer : ITimer
    {
        private readonly NetTimer _timer = new();

        // converting the simple ITimer handler to the .net timer one
        private readonly IDictionary<ElapsedHandler, ElapsedEventHandler> _handlers =
            new Dictionary<ElapsedHandler, ElapsedEventHandler>();

        public event ElapsedHandler Elapsed {
            add {
                void timerHandler(object sender, ElapsedEventArgs e) => value();
                _handlers.Add(value, timerHandler);
                _timer.Elapsed += timerHandler;
            }
            remove {
                var timerHandler =_handlers[value];
                _timer.Elapsed -= timerHandler;
                _handlers.Remove(value);
            }
        }

        private static object _lock = new();


        public void Start(double ms)
        {
            _timer.Interval = ms;
            _timer.Start();
        }

        public void RunNowAndStart(double ms)
        {
            foreach (var handler in _handlers)
            {
                handler.Key.Invoke();
            }

            Start(ms);
        }

        public void RunInAndStart(double runIn, double ms)
        {
            _timer.Interval = runIn;
            _timer.AutoReset = false;

            void changeIntervalsOver(object sender, ElapsedEventArgs e)
            {
                _timer.Interval = ms;
                _timer.Elapsed -= changeIntervalsOver;
                _timer.AutoReset = true;
            }
            _timer.Elapsed += changeIntervalsOver;

            _timer.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
        }

        public void Dispose()
        {
            Stop();
            _timer.Dispose();
        }
    }
}
