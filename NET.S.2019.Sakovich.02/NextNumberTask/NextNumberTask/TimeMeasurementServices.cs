using System;

namespace NextNumberTask
{
    public interface IStopwatch
    {
        void Start();
        void Stop();
        void Reset();

        bool IsRunning { get; }
        TimeSpan Elapsed { get; }
    }
}