using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTask
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            SimpleTimer simpleTimer1 = new SimpleTimer(new TimeSpan(0, 0, 0, 10).Ticks, 1000 * 10000);

            Console.CancelKeyPress += (sender, e) => simpleTimer1.Stop();

            simpleTimer1.CountdownInterrupted += (sender, e) => Console.WriteLine("Interrupt! Time = {0}", TimeSpan.FromTicks(e.Ticks));
            simpleTimer1.TimeElapsed += (sender, e) => Console.WriteLine("Finished.");

            simpleTimer1.Start();

            while (simpleTimer1.IsTicking)
            {
                Console.WriteLine(TimeSpan.FromTicks(simpleTimer1.Ticks));
                Thread.Sleep(1000);
            }

            Console.ReadKey();
        }
    }
}
