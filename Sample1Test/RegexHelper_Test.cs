using NUnit.Framework;
using TakeAsh;

namespace Sample1Test {
    [TestFixture]
    public class RegexHelper_Test {

        public struct TestDataSet {
            public string input { get; set; }
            public string expected { get; set; }

            public TestDataSet(string input, string expected) : this() {
                this.input = input;
                this.expected = expected;
            }
        };

        static TestDataSet[] testData1 = {
            new TestDataSet(null, null),
            new TestDataSet("", ""),
            new TestDataSet("0123456789", "0123456789"),
            new TestDataSet("ABCabcXYZxyz", "ABCabcXYZxyz"),
            new TestDataSet("_", "_"),
            new TestDataSet(" ", "\\u0020"),
            new TestDataSet("\t", "\\u0009"),
            new TestDataSet("\n", "\\u000a"),
            new TestDataSet("\r", "\\u000d"),
            new TestDataSet("\r\n", "\\u000d\\u000a"),
            new TestDataSet("012\t345\n678\r9", "012\\u0009345\\u000a678\\u000d9"),
            new TestDataSet("あいう", "\\u3042\\u3044\\u3046"),
            new TestDataSet("高髙崎﨑", "\\u9ad8\\u9ad9\\u5d0e\\ufa11"),
            new TestDataSet("剥\u525D填\u5861頬\u9830", "\\u5265\\u525d\\u586b\\u5861\\u982c\\u9830"),
            new TestDataSet("\uD842\uDF9F", "\\ud842\\udf9f"),  // U+20B9F 𠮟
            new TestDataSet("\uD842\uDFB7", "\\ud842\\udfb7"),  // U+20BB7 𠮷
        };

        static object[] testData2 = NUnitHelper<TestDataSet>.makeTestCases(testData1);

        static string[] inputs;
        static string[] expecteds;

        [TestFixtureSetUp]
        public void setupOnce() {
            var length = testData1.Length;
            inputs = new string[length];
            expecteds = new string[length];
            for (var i = 0; i < length; ++i) {
                inputs[i] = testData1[i].input;
                expecteds[i] = testData1[i].expected;
            }
        }

        [TestCaseSource("testData2")]
        public void Quotemeta_string_Test(string input, string expected) {
            Assert.AreEqual(expected, RegexHelper.Quotemeta(input));
        }

        [TestCase]
        public void Quotemeta_stringArray_Test() {
            CollectionAssert.AreEqual(expecteds, RegexHelper.Quotemeta(inputs));
        }
    }
}
