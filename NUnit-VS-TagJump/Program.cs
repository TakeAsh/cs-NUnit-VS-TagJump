using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TakeAsh {
    class Program {
        static void Main(string[] args) {
            Regex regTestFailure = new Regex(@"Test\sFailure\s*:\s*(?<target>[\s\S]+)$");
            Regex regExpected = new Regex(@"Expected\s*:\s*(?<target>[\s\S]+)$");
            Regex regActual = new Regex(@"But\swas\s*:\s*(?<target>[\s\S]+)$");
            Regex regLineInFile = new Regex(@"^(?<head>[\s\S]*)\s+(in|場所)\s(?<file>([a-zA-Z]:)?[^:]+?):(line|行)\s(?<line>\d+)$");
            Regex regTestError = new Regex(@"Test\sError\s*:\s*(?<target>[\s\S]+)$", RegexOptions.IgnoreCase);
            Regex regNotRunnable = new Regex(@"NotRunnable\s*:\s*(?<target>[\s\S]+)$");
            string testCase = "";
            string expected = "";
            string actual = "";
            string line2 = "";
            int errorCount = 0;
            int notRunCount = 0;
            try {
                using (TextReader input = args.Length == 0 ?
                    Console.In :
                    new StreamReader(args[0])) {
                    string line;
                    while ((line = input.ReadLine()) != null) {
                        if (checkMatch(line, regTestFailure, ref testCase) ||
                            checkMatch(line, regExpected, ref expected) ||
                            checkMatch(line, regActual, ref actual)) {
                            Console.WriteLine(line);
                        } else if (checkMatchLineInFile(line, regLineInFile, ref line2)) {
                            Console.WriteLine(
                                "{0}: assert error : TestCase:{1}, Expected:{2}, Actual:{3}",
                                line2, testCase, expected, actual
                            );
                            line2 = testCase = expected = actual = "";
                            ++errorCount;
                        } else if (checkMatch(line, regTestError, ref testCase)) {
                            var errorMessage = input.ReadLine();
                            Console.WriteLine(
                                "{0}) NUnit TestError: Test Error : TestCase:{1},{2}",
                                ++errorCount, testCase, errorMessage
                            );
                        } else if (checkMatch(line, regNotRunnable, ref testCase)) {
                            var errorMessage = input.ReadLine();
                            Console.WriteLine(
                                "{0}) NUnit NotRun: NotRun Error : TestCase:{1},{2}",
                                ++notRunCount, testCase, errorMessage
                            );
                        } else {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
            }
            Environment.Exit(errorCount + notRunCount);
        }

        static bool checkMatch(string str, Regex reg, ref string output) {
            MatchCollection mc = reg.Matches(str);
            if (mc.Count > 0) {
                Match m = mc[0];
                output = m.Groups["target"].Value;
                return true;
            } else {
                return false;
            }
        }

        static bool checkMatchLineInFile(string str, Regex reg, ref string output) {
            MatchCollection mc = reg.Matches(str);
            if (mc.Count > 0) {
                Match m = mc[0];
                output = String.Format(
                    "{0}\n{1}({2})",
                    m.Groups["head"].Value, m.Groups["file"].Value, m.Groups["line"].Value
                );
                return true;
            } else {
                return false;
            }
        }
    }
}
