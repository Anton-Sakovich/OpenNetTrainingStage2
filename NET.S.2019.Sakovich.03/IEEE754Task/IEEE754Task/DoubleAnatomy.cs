﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public class DoubleAnatomy : IFPointAnatomy<double>
    {
        public int FullLength { get => 64; }

        public int MantissaLength { get => 52; }

        public int ExponentLength { get => 11; }

        public int ExponentShift { get => _ExponentShift; }

        public double Zero { get => 0D; }

        public double One { get => 1D; }

        public double MinNormalNumber { get => _MinNormalNumber; }

        static readonly int _ExponentShift;

        static readonly double _MinNormalNumber;

        static DoubleAnatomy()
        {
            _ExponentShift = (1 << 10) - 1;

            _MinNormalNumber = 2.0;

            for (int i = 0; i < _ExponentShift; i++)
                _MinNormalNumber /= 2.0;
        }
    }
}