using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public abstract class BuffEffect : ContextDependent {

		public abstract void ApplyOn(Character target);
		public abstract void RemoveFrom(Character target);
	}
}
