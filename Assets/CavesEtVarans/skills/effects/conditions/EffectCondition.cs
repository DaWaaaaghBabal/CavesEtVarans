using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.conditions {
	public abstract class EffectCondition {
		public abstract bool Match(Character target, Context context);
	}
}