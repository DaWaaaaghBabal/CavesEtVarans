using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.effects.conditions;

namespace CavesEtVarans.skills.effects {
	public abstract class TargetedEffect : ContextDependent, IEffect {
		private IEffectFilter filter;
		public IEffectFilter Filter {
			set {
				filter = value;
			}
			get {
				if (filter == null) {
					filter = new NoCondition();
				}
				return filter;
			}
		}
		public string TargetKey { set; get; }

		public void Apply(Context context) {
			TargetGroup targets = (TargetGroup) ReadContext(context, Context.TARGETS + TargetKey);
            if (targets != null) {
                foreach (Character target in targets) {
					if (Filter.Match(target, context))
                    context.Set(Context.CURRENT_TARGET, target);
                    Apply(target, context);
                }
            }
		}
		public abstract void Apply(Character target, Context context);
	}
}
