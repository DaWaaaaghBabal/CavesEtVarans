using CavesEtVarans.character;

namespace CavesEtVarans.utils {
    public class Entry<T1, T2> {
        private Character target;
        private int amount;

        public Entry(T1 key, T2 value) {
            Key = key;
            Value = value;
        }

        public T1 Key { set; get; }
        public T2 Value { set; get; }
    }
}