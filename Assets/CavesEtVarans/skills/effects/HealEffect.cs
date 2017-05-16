using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class HealEffect : TargetedEffect {
		public IValueCalculator Amount { get; set; }
		
		public override void Apply(Character target) {
            Character source = (Character) ReadContext(ContextKeys.SOURCE);
            int amount = (int)Amount.Value();
            target.TakeDamage(-amount);
            new HealEvent(source, target, amount).Trigger();
		}
	}
}
