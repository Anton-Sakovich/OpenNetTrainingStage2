using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GCDTask.Tests
{
    [TestFixture]
    public class GCDBinaryEuclidTests : GCDTestsBase
    {
        public GCDBinaryEuclidTests() : base(new GCDBinaryEuclid()) { }
    }
}
