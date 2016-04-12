using System;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public abstract class BuffInstance : ContextDependent {
		public Duration Duration { set; get; }

		public void Tick() {
			if (Duration.Tick() == 0) {
				// We know that the buff target is in the private context, and thus need no external context, hence null
				Character target = (Character) ReadContext(null, Context.BUFF_TARGET);
				RemoveFrom(target);
			}
		}

		public void RemoveFrom(Character target) {
			target.RemoveBuff(this);
		}

		public abstract void ApplyOn(Character c);
	}
}
