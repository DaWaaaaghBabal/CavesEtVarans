using System;
using System.Collections.Generic;

namespace CavesEtVarans.utils {
    public class DualKeyDictionary<K1, K2, V> {
        private Dictionary<K1, Dictionary<K2, V>> data;

        public bool IsEmpty { get; private set; }

        public DualKeyDictionary() {
            data = new Dictionary<K1, Dictionary<K2, V>>();
            IsEmpty = true;
        }

        public void Add(K1 k1, K2 k2, V v) {
            if (!data.ContainsKey(k1))
                data.Add(k1, new Dictionary<K2, V>());
            if(!data[k1].ContainsKey(k2))
                data[k1].Add(k2, v);
            IsEmpty = false;
        }

        public bool ContainsKeys(K1 k1, K2 k2) {
            if (!data.ContainsKey(k1)) return false;
            return data[k1].ContainsKey(k2);
        }

        public Dictionary<K2, V> this[K1 k1]{
            private set{}
            get { return data[k1]; }
        }

        public void ForEachDo(Action<K1, K2, V> action) {
            foreach(KeyValuePair<K1, Dictionary<K2, V>> line in data) {
                foreach (KeyValuePair<K2, V> entry in line.Value) {
                    action(line.Key, entry.Key, entry.Value);
                }
            }
        }
    }
}
