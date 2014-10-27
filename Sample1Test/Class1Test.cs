using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Sample1;
using TakeAsh;

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
            Assert.AreEqual(expected, Class1.max(a, b));
        }

        public struct IntTestCase {
            public int a { get; set; }
            public int b { get; set; }
            public int expected { get; set; }

            public IntTestCase(int a, int b, int expected) : this() {
                this.a = a;
                this.b = b;
                this.expected = expected;
            }
        }

        static IntTestCase[] intTestCase1 = {
            new IntTestCase(1, 3, 1),   // test fail
            new IntTestCase(1, 3, 3),
            //new IntTestCase(3, 1, 1.0), // can't compile
            new IntTestCase(3, 1, 3),
            //new IntTestCase(1, 2),      // can't compile
        };

        static object[] intTestCase2 = NUnitHelper<IntTestCase>.makeTestCases(intTestCase1);

        [Test, TestCaseSource("intTestCase2")]
        public void maxTest_int2(int a, int b, int expected) {
            Assert.AreEqual(expected, Class1.max(a, b));
        }

        public struct DoubleTestCase {
            public double a { get; set; }
            public double b { get; set; }
            public double expected { get; set; }

            public DoubleTestCase(double a, double b, double expected) : this() {
                this.a = a;
                this.b = b;
                this.expected = expected;
            }
        }

        static DoubleTestCase[] doubleTestCase1 = {
            new DoubleTestCase(1.0, 3.0, 1.0),  // test fail
            new DoubleTestCase(1.0, 3.0, 3.0),
            new DoubleTestCase(3.0, 1.0, 1.0),  // test fail
            new DoubleTestCase(3.0, 1.0, 3.0),
        };

        static object[] doubleTestCase2 = NUnitHelper<DoubleTestCase>.makeTestCases(doubleTestCase1);

        [Test, TestCaseSource("doubleTestCase2")]
        public void maxTest_double2(double a, double b, double expected) {
            Assert.AreEqual(expected, Class1.max(a, b));
        }

        [Test, TestCaseSource(typeof(TestDataProvider), "Int_TestCases")]
        public int maxTest_int3(
            string no,
            int a,
            int b
        ) {
            return Class1.max(a, b);
        }
    }

    static public class TestDataProvider {
        private const string dataFile = "../TestData/IntTestCases.txt";

        static public IEnumerable<TestCaseData> Int_TestCases {
            get {
                foreach (var line in File.ReadLines(dataFile, Encoding.UTF8)) {
                    if (line[0] == '#') {
                        continue;
                    }
                    var items = line.Split('\t');
                    yield return new TestCaseData(
                        items[0],
                        Int32.Parse(items[1]),
                        Int32.Parse(items[2])
                    ).Returns(
                        Int32.Parse(items[3])
                    );
                }
            }
        }
    }
}
