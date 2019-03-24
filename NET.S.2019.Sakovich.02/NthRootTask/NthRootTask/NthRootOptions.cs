using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NthRootTask
{
    public struct NthRootOptions
    {
        public double AbsoluteErrorGoal;
        public double RelativeErrorGoal;
        public uint MaxStepsCount;
    }
}
