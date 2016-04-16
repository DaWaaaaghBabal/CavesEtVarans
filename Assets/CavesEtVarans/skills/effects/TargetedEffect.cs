using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.skills.effects {
	public abstract class TargetedEffect : ContextDependent, IEffect {
	
		public string TargetKey { set; get; }

		public void Apply(Context context) {
			TargetGroup targets = (TargetGroup) ReadContext(context, Context.TARGETS + TargetKey);
            if (targets != null) {
                foreach (Character target in targets) {
                    context.Set(Context.CURRENT_TARGET, target);
                    Apply(target, context);
                }
            }
		}
		public abstract void Apply(Character target, Context context);
	}
}
