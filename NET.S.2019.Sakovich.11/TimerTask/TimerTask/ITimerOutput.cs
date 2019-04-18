using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTask
{
    internal interface ITimerOutput
    {
        long Ticks { get; }

        bool IsTicking { get; }

        event EventHandler<TimerEventArgs> TimeElapsed;

        event EventHandler<TimerEventArgs> CountdownInterrupted;
    }
}
