using System;
using System.Text.RegularExpressions;

namespace TakeAsh {
    static public class RegexHelper {
        static private Regex _regQuotemeta = new Regex(@"([^0-9A-Za-z_])");

        /// <summary>
        /// escape non-word characters as unicode expression.
        /// </summary>
        /// <param name="source">raw string</param>
        /// <returns>escaped string</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item><a href="http://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.escape.aspx">Regex.Escape Method</a></item>
        /// <item><a href="http://blog.livedoor.jp/dankogai/archives/51058313.html">quotemeta for javascript</a></item>
        /// </list>
        /// </remarks>
        static public string Quotemeta(string source) {
            if (String.IsNullOrEmpty(source)) {
                return source;
            }
            return _regQuotemeta.Replace(
                source,
                (Match m) => "\\u" + String.Format("{0:x4}", (int)(m.Value[0]))
            );
        }

        /// <summary>
        /// escape non-word characters as unicode expression.
        /// </summary>
        /// <param name="source">raw strings</param>
        /// <returns>escaped strings</returns>
        static public string[] Quotemeta(string[] source) {
            var ret = new string[source.Length];
            for (var i = 0; i < source.Length; ++i) {
                ret[i] = Quotemeta(source[i]);
            }
            return ret;
        }
    }
}
