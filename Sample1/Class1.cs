using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample1 {
    static public class Class1 {
        static public T max<T>(T a, T b)
            where T : IComparable {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
}
