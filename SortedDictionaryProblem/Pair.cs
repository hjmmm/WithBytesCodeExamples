using System;
using System.Collections.Generic;

namespace SortedDictionaryProblem {
    class Pair : IComparable{
        protected int a;
        protected int b;

        public Pair(int a, int b) {
            this.a = a;
            this.b = b;
        }

        #region IComparable Members

        public int CompareTo(object obj) {
            Pair right;
            if (!(obj is Pair)){
                throw new ArgumentException("The argument is not a Pair");
            }
            if (obj == null) {
                return int.MaxValue;
            }
            right = (Pair)obj;
            if (a != right.a) {
                return a.CompareTo(right.a);
            }
            return b.CompareTo(right.b);            
        }

        #endregion

        public override string ToString() {
            return "<" + a + "," + b + ">";
        }

    }
}
