using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public struct NonNegativeInteger
    {
        public int Value { get; private set; }

        public NonNegativeInteger(int val)
        {
            if(val < 0)
            {
                throw new ArgumentException();
            }

            Value = val;
        }

        public void Add(int val)
        {
            if(val > 0)
            {
                Value += val;
            }

            if(val < 0)
            {
                Value = Math.Max(0, Value + val);
            }
        }

        public void Take(int val)
        {
            if(val < 0)
            {
                Value -= val;
            }

            if(val > 0)
            {
                Value = Math.Max(0, Value - val);
            }
        }

        public static explicit operator NonNegativeInteger(int val)
        {
            try
            {
                return new NonNegativeInteger(val);
            }
            catch (ArgumentException)
            {
                throw new InvalidCastException();
            }
        }
    }
}
