using CavesEtVarans.utils;
using System.Collections.Generic;
using CavesEtVarans.character;

namespace CavesEtVarans.skills.core {
    public class EffectResult : List<Entry<ITargetable, int>> { // TODO : Wrap around a list instead of being one
       public static EffectResult operator +(EffectResult er1, EffectResult er2) {
            EffectResult sum = new EffectResult();
            foreach (Entry<ITargetable, int> t in er1) sum.Add(t);
            foreach (Entry<ITargetable, int> t in er2) sum.Add(t);
            return sum;
        }
        public static EffectResult Void = new EffectResult();
        public int Total { set; get; }

        public EffectResult() : base() {
            Total = 0;
        }

        public EffectResult(Character target, int amount) {
            Add(new Entry<ITargetable, int>(target, amount));
        }

        new public void Add(Entry<ITargetable, int> t) {
            Total += t.Value;
            base.Add(t);
        }
        new public void Remove(Entry<ITargetable, int> t) {
            if (Contains(t))
                Total += t.Value;
            base.Remove(t);
        }
    }
}