using System;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.events
{
    public class DamageEvent : GameEvent<DamageEvent>
    {
        public int Amount;
        public Character Source;
        public Character Target;

        public DamageEvent(Character source, Character target, int amount)
        {
            this.Source = source;
            this.Target = target;
            this.Amount = amount;
        }

		public override TriggerType TriggerType() {
			return events.TriggerType.Damage;
		}
	}
}