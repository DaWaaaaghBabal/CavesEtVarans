using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class DamageEffect : TargetedEffect {
		public IValueCalculator Amount { get; set; }
        public string Type { set; get; }
		public override void Apply(Character target) {
            SetLocalContext(ContextKeys.DAMAGE_TYPE, Type);
            Character source = (Character) ReadContext(ContextKeys.SOURCE);
            int amount = (int)Amount.Value();
			target.TakeDamage(amount);
            new DamageEvent(source, target, amount).Trigger();
		}
	}
}