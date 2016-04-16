using System;
using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
    public class StatisticBuffInstance : BuffInstance {

        public StatisticBuffInstance(IValueCalculator calculator, ModifierType type) {
            IStatModifier modifier = StatModifier.New(ModifierType);
        }

        public override void ApplyOn(Character c) {
            throw new NotImplementedException();
        }

        public override void RemoveFrom(Character target) {
            throw new NotImplementedException();
        }
    }
}