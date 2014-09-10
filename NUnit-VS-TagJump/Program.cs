using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TakeAsh {
    class Program {
        static void Main(string[] args) {
            Regex regTestCase = new Regex(@"Test\sFailure\s*:\s*(?<target>[\s\S]+)$");
            Regex regExpected = new Regex(@"Expected\s*:\s*(?<target>[\s\S]+)$");
            Regex regActual = new Regex(@"But\swas\s*:\s*(?<target>[\s\S]+)$");
            Regex regLineInFile = new Regex(@"^(?<head>[\s\S]*)\s+(in|場所)\s(?<file>([a-zA-Z]:)?[^:]+?):(line|行)\s(?<line>\d+)$");
            string testCase = "";
            string expected = "";
            string actual = "";
            int errorCount = 0;
            try {
                using (TextReader input = args.Length == 0 ?
                    Console.In :
                    new StreamReader(args[0])) {
                    string line;
                    while ((line = input.ReadLine()) != null) {
                        if (checkMatch(line, regTestCase, ref testCase) ||
                            checkMatch(line, regExpected, ref expected) ||
                            checkMatch(line, regActual, ref actual)) {
                            Console.WriteLine(line);
                            continue;
                        }
                        MatchCollection mc = regLineInFile.Matches(line);
                        if (mc.Count > 0) {
                            Match m = mc[0];
                            Console.WriteLine(
                                "{0}\n{1}({2}): assert error : TestCase:{3}, Expected:{4}, Actual:{5}",
                                m.Groups["head"].Value, m.Groups["file"].Value, m.Groups["line"].Value,
                                testCase, expected, actual
                            );
                            testCase = expected = actual = "";
                            ++errorCount;
                        } else {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
            }
            Environment.Exit(errorCount);
        }

        static bool checkMatch(string str, Regex reg, ref string variable) {
            MatchCollection mc = reg.Matches(str);
            if (mc.Count > 0) {
                Match m = mc[0];
                variable = m.Groups["target"].Value;
                return true;
            } else {
                return false;
            }
        }
    }
}
