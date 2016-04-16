using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using UnityEngine;

namespace CavesEtVarans.skills.effects {
	public class DamageEffect : TargetedEffect {
		public IValueCalculator Amount { get; set; }
        public string Type { set; get; }
		public override void Apply(Character target, Context context) {
            context.Set(Context.DAMAGE_TYPE, Type);
            Character source = (Character) ReadContext(context, Context.SOURCE);
            int amount = (int)Amount.Value(context);
			target.TakeDamage(amount);
            Debug.Log(Battlefield.Flanking(source, target));
            new DamageEvent(source, target, amount).Trigger(context);
		}
	}
}