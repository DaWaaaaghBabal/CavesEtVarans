using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class HealEffect : TargetedEffect {
		public IValueCalculator Amount { get; set; }
		
		public override void Apply(Character target, Context context) {
            Character source = (Character) ReadContext(context, Context.SOURCE);
            int amount = (int)Amount.Value(context);
            target.TakeDamage(-amount);
            new HealEvent(source, target, amount).Trigger(context);
		}
	}
}
