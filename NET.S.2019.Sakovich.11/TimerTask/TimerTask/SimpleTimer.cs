using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTask
{
    internal class SimpleTimer
    {
        // This is what SimpleTimer "displays".
        private long _ticks;

        // This is the length of one SimpleTimer's tick
        private long _step;

        // This is what tells us whether SimpleTimier is running.
        private bool _isTicking;

        // This is a Task in which a SimpleTimer object runs
        private Task _tickingTask;

        // This lock is used to ensure that no thread can start or stop SimpleTimer before a pending
        // start/stop request is completed.
        private readonly object _startStopLock = new object();

        public SimpleTimer(long ticks, long step)
        {
            _ticks = ticks;
            _step = step;
            _isTicking = false;
        }

        public event EventHandler<TimerEventArgs> TimeElapsed;

        public event EventHandler<TimerEventArgs> CountdownInterrupted;

        public long Ticks { get => _ticks < 0L ? 0L : _ticks; }

        public long Step { get => _step; }

        public bool IsTicking { get => _isTicking; }

        public Task TickingTask { get => _tickingTask; }

        public SimpleTimer Set(long newTicks)
        {
            lock (_startStopLock)
            {
                Stop();
                _ticks = newTicks;
            }

            return this;
        }

        public void Start()
        {
            lock (_startStopLock)
            {
                if (_isTicking)
                {
                    return;
                }
                else
                {
                    _isTicking = true;
                }

                _tickingTask = Task.Run(new Action(Tick));
            }
        }

        public void Stop()
        {
            lock (_startStopLock)
            {
                if (_isTicking)
                {
                    _isTicking = false;

                    _tickingTask?.Wait();
                }
            }
        }

        private void Tick()
        {
            while (_isTicking && (_ticks > 0))
            {
                _ticks -= _step;
                Thread.Sleep((int) (_step / 10000L));
            }

            if (_isTicking)
            {
                _isTicking = false;
                OnTimeElapsed();
            }
            else
            {
                OnCountdownInterrupted();
            }
        }

        private void OnTimeElapsed()
        {
            TimeElapsed?.Invoke(this, new TimerEventArgs(_ticks));
        }

        private void OnCountdownInterrupted()
        {
            CountdownInterrupted?.Invoke(this, new TimerEventArgs(_ticks));
        }
    }
}
