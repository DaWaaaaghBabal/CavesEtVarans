using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class DamageEffect : TargetedEffect {
		public IValueCalculator Amount { get; set; }
        public string Type { set; get; }
		public override EffectResult Apply(Character target, int suffix) {
            SetLocalContext(ContextKeys.DAMAGE_TYPE, Type);
            Character source = (Character) ReadContext(ContextKeys.SOURCE);
            int amount = (int)Amount.Value();
			amount = target.TakeDamage(amount);
            new DamageEvent(source, target, amount).Trigger();
            return new EffectResult(target, amount);
		}

        public override EffectResult Apply(Tile tile, int suffix) {
            throw new NotImplementedException("Trying to damage a tile ! Please check YAML for inconsistencies.");
        }
    }
}