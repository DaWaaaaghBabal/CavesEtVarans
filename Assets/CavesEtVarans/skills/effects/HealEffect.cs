using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class HealEffect : TargetedEffect {
		public IValueCalculator Amount { get; set; }
		
		public override EffectResult Apply(Character target, int suffix) {
            Character source = (Character) ReadContext(ContextKeys.SOURCE);
            int amount = (int)Amount.Value();
            target.TakeDamage(-amount);
            new HealEvent(source, target, amount).Trigger();
            return new EffectResult(target, amount);
		}

        public override EffectResult Apply(Tile tile, int suffix) {
            throw new NotImplementedException("Trying to heal a tile ! Please check YAML for inconsistencies.");
        }
    }
}
