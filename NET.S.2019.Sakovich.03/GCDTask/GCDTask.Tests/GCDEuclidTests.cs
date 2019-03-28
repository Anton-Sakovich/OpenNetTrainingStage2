using System;
using System.Collections.Generic;
using GCDTask;
using NUnit.Framework;

namespace GCDTask.Tests
{
    [TestFixture]
    public class GCDEuclidTests : GCDTestsBase
    {
        public GCDEuclidTests() : base(new GCDEuclid()) { }
    }
}
