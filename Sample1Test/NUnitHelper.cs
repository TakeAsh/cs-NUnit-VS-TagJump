using System.Collections.Generic;
using System.Reflection;

namespace TakeAsh {
    static public class NUnitHelper<T> {
        static private PropertyInfo[] _properties;

        static NUnitHelper() {
            var properties = new List<PropertyInfo>();
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            foreach (var prop in typeof(T).GetProperties(flags)) {
                if (prop.CanRead == true && prop.GetGetMethod() != null) {
                    properties.Add(prop);
                }
            }
            _properties = properties.ToArray();
        }

        /// <summary>
        /// get all properties as object[].
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>array of porperties</returns>
        static public object[] getArgumentsAndResult(T obj) {
            var ret = new object[_properties.Length];
            for (var i = 0; i < _properties.Length; ++i) {
                ret[i] = _properties[i].GetValue(obj, null);
            }
            return ret;
        }

        /// <summary>
        /// make TestCaseSource as object[][].
        /// </summary>
        /// <param name="source">array of TestCase Data</param>
        /// <returns>TestCases as object[][]</returns>
        static public object[] makeTestCases(T[] source) {
            var ret = new object[source.Length];
            for (var i = 0; i < source.Length; ++i) {
                ret[i] = NUnitHelper<T>.getArgumentsAndResult(source[i]);
            }
            return ret;
        }
    }
}
