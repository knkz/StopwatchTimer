using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTimer
{
    internal class StopwatchTimer
    {
        private const int MINIMUM_DELAY = 30;

        private Stopwatch _stopWatch = new();

        private bool _enabled;

        private Action _callback;

        private int _interval;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="callback"></param>
        public StopwatchTimer(int interval, Action callback)
        {
            _interval = interval;
            _callback = callback;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            _enabled = true;
            StartTimer();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _enabled = false;
        }

        private void StartTimer()
        {
            _ = Task.Run(async () =>
            {
                _stopWatch.Restart();
                while (_enabled)
                {
                    var msec = _stopWatch.ElapsedMilliseconds;
                    var rest = _interval - (int)(msec / _interval);

                    if (rest > MINIMUM_DELAY)
                    {
                        await Task.Delay(rest - MINIMUM_DELAY);
                    }

                    while (true)
                    {
                        if (_stopWatch.ElapsedMilliseconds >= msec + _interval)
                        {
                            break;
                        }
                        Thread.Sleep(0);
                    }
                    _callback.Invoke();
                }
                _stopWatch.Stop();
            });
        }
    }
}
