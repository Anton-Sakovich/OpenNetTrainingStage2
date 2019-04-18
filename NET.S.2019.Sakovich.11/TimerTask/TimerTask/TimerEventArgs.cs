using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTask
{
    internal class TimerEventArgs : EventArgs
    {
        public long Ticks { get; }

        public TimerEventArgs(long ticks)
        {
            Ticks = ticks;
        }
    }
}
