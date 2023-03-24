namespace FFS.Time.Timer
{
    internal class TimerFactory : ITimerFactory
    {
        public ITimer Create()
        {
            return new FfsTimer();
        }
    }
}
