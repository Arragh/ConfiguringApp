using System.Diagnostics;

namespace ConfiguringApp.Infrastructure
{
    public class UptimeService
    {
        private Stopwatch timer;

        public UptimeService() => timer = Stopwatch.StartNew();

        public long Uptime => timer.ElapsedMilliseconds;
    }
}
