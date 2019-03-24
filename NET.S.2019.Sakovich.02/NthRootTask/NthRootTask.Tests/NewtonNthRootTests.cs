using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NthRootTask;
using NUnit.Framework;

namespace NthRootTask.Tests
{
    [TestFixture]
    public class NewtonNthRootTests
    {
        static IEnumerable<NthRootTestData> RandomRoots
        {
            get
            {
                yield return new NthRootTestData() { Y = 99.79714834007052, N = 4, X = 3.1606727556298 };
                yield return new NthRootTestData() { Y = 32.68553365436563, N = 9, X = 1.4732000710127198 };
                yield return new NthRootTestData() { Y = 35.19384424503997, N = 7, X = 1.663120877512603 };
                yield return new NthRootTestData() { Y = 91.84228880151377, N = 3, X = 4.5117763777259725 };
                yield return new NthRootTestData() { Y = 14.957187137361828, N = 6, X = 1.5696698671496385 };
                yield return new NthRootTestData() { Y = 13.823625396228238, N = 8, X = 1.3886018660929713 };
                yield return new NthRootTestData() { Y = 18.526575368537436, N = 7, X = 1.5174472136059618 };
                yield return new NthRootTestData() { Y = 62.363807506982596, N = 3, X = 3.9656179721823963 };
                yield return new NthRootTestData() { Y = 98.96916745714262, N = 2, X = 9.94832485683608 };
                yield return new NthRootTestData() { Y = 5.6753131842507685, N = 6, X = 1.3355648238695499 };
            }
        }

        [Test]
        public void RandomTestsWithAccuracyOnly()
        {
            NewtonNthRoot Solver1 = new NewtonNthRoot(new NthRootOptions { RelativeErrorGoal = 0.0, AbsoluteErrorGoal = 1.0e-10, MaxStepsCount = 1000 });

            foreach(NthRootTestData RootCase in RandomRoots)
            {
                double Expected = RootCase.X;
                double Actual = Solver1.FindRoot(RootCase.Y, RootCase.N);
                Assert.That(Solver1.AreEqual(Expected, Actual));
            }
        }
    }

    public struct NthRootTestData
    {
        public double X;
        public double Y;
        public int N;
    }
}
