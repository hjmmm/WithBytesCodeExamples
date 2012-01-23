using System;
using System.Collections.Generic;

namespace SortedDictionaryProblem {
    class ProblematicPair : IComparable{
        private int a;
        private int b;

        public ProblematicPair(int a, int b) {
            this.a = a;
            this.b = b;
        }

        #region IComparable Members

        public int CompareTo(object obj) {
            ProblematicPair right;
            if (!(obj is ProblematicPair)){
                throw new ArgumentException("The argument is not a Pair");
            }
            if (obj == null) {
                return int.MaxValue;
            }
            right = (ProblematicPair)obj;
            if (a != right.a) {
                return a.CompareTo(right.a);
            }
            return 1;
        }

        #endregion

        public override string ToString() {
            return "<"+a+","+b+">";
        }
    }
}
