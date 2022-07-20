using System.Timers;

namespace PerformanceTimer
{
    internal class Program
    {
        static System.Timers.Timer _timer;
        static StopwatchTimer _pTimer;
        static int _count;

        static void Main(string[] args)
        {
            //_timer = new System.Timers.Timer
            //{
            //    AutoReset = true,
            //    Interval = 10
            //};
            //_timer.Elapsed += Timer_Elapsed;
            //_timer.Start();
            _pTimer = new StopwatchTimer(10, () => ElapsedImpl());
            _pTimer.Start();
            var _ = Console.ReadLine();
        }

        private static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            ElapsedImpl();
        }

        private static void ElapsedImpl()
        {
            if (_count > 100)
            {
                _timer?.Stop();
                _pTimer?.Stop();
                return;
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.ffff"));
            _count++;
        }
    }
}