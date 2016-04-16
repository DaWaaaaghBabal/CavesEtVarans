using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
    public class StatisticBuff : BuffEffect {

        public IValueCalculator Amount;
        public string StatisticKey;
        public override BuffInstance Instantiate(Context context) {
            StatisticBuffInstance instance = new StatisticBuffInstance();

        }
    }
}
