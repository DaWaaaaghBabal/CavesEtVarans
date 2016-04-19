using System.Collections.Generic;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.conditions {
	
	public interface IEffectFilter {
		bool Match(Character target, Context context);
	}

	public class EffectFilter : List<EffectCondition>, IEffectFilter {
		public bool Match(Character target, Context context) {
			bool result = true;
			foreach (EffectCondition condition in this) {
				result &= condition.Match(target, context);
			}
			return result;
		}
	}
	public class NoCondition : IEffectFilter {
		public bool Match(Character target, Context context) {
			return true;
		}
	}
}