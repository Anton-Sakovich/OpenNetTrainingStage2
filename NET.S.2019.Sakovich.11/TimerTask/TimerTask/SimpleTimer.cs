using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTask
{
    internal class SimpleTimer
    {
        private TimeSpan _now;

        private bool _isTicking;

        public SimpleTimer(TimeSpan initialTime)
        {
            _now = initialTime;
            _isTicking = false;
        }

        public TimeSpan Now { get => _now; }

        public bool IsTicking { get => _isTicking; }

        public SimpleTimer Set(TimeSpan newTime)
        {
            // It is important to stop the timer before changing its state manually
            // because it can be counting down.
            Stop();
            _now = newTime;

            return this;
        }

        public Task Start()
        {
            Action tickingAction = () =>
            {
                _isTicking = true;

                while (_isTicking && _now.Milliseconds > 0)
                {
                    Tick();
                }

                _isTicking = false;
            };

            Task tickingTask = Task.Run(tickingAction);

            return tickingTask;
        }

        public void Stop()
        {
            _isTicking = false;
        }

        private void Tick()
        {
            _now.Subtract(new TimeSpan(0, 0, 0, 0, 100));
        }
    }
}
