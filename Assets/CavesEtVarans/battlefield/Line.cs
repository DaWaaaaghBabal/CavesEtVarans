using System;
using System.Collections;
using System.Collections.Generic;

namespace CavesEtVarans.battlefield {
    public class Line<T> : IEnumerable<T> where T : ICoordinates {
        /// <summary>
        /// Must return a negative number if t1 is "lesser than" t2, 
        /// a positive number if t1 is "greater than" t2, and 0 if they're equivalent.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public delegate int Comparison(T t1, T t2);
        public Comparison Compare { private get; set; }
        private IEnumerable<T> [] content;

        public int Length { get { return content.Length; } private set { } }

        public Line(int length) {
            content = new IEnumerable<T>[Math.Max(0, length)];
            Compare = DefaultComparison;
        }

        public void Add(IEnumerable<T> list, int i) {
            if (list != null)
                content[i] = list;
        }

        public T this[int i] {
            private set { }
            get {
                if (i < content.Length) {
                    IEnumerable<T> enumerable = content[i];
                    if (enumerable == null) return default(T);
                    T currentHighest = default(T);
                    foreach (T t in enumerable) {
                        if (Compare (t, currentHighest) >= 0) {
                            currentHighest = t;
                        }
                    }
                    return currentHighest;
                } else throw new IndexOutOfRangeException();
            }
        }

        private int DefaultComparison(T t1, T t2) {
            return 0;
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0 ; i < content.Length ; i++) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}