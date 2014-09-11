using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sample1;

namespace Sample1Test {
    [TestFixture]
    public class Class1Test {

        [TestCase(1, 3, 1)]   // test fail
        [TestCase(1, 3, 3)]
        [TestCase(3, 1, 1.0)]   // test error
        [TestCase(3, 1, 3)]
        [TestCase(1, 2)]        // NotRunnable, Wrong number of arguments
        public void maxTest_int(int a, int b, int expected) {
            Assert.AreEqual(expected, Class1.max(a, b));
        }

        [TestCase(1.0, 3.0, 1.0)]   // test fail
        [TestCase(1.0, 3.0, 3.0)]
        [TestCase(3.0, 1.0, 1.0)]   // test fail
        [TestCase(3.0, 1.0, 3.0)]
        public void maxTest_double(double a, double b, double expected) {
            Assert.AreEqual(expected,Class1.max(a,b));
        }
    }
}
