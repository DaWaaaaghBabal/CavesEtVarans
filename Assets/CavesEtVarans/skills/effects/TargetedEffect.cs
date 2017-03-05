using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.effects.conditions;
using CavesEtVarans.skills.filters;

namespace CavesEtVarans.skills.effects {
	public abstract class TargetedEffect : ContextDependent, IEffect {
		private FiltersList filter;
		public FiltersList Filter {
			set {
				filter = value;
			}
			get {
				if (filter == null) {
					filter = new FiltersList();
				}
				return filter;
			}
		}
		public string TargetKey { set; get; }

		public void Apply(Context context) {
			TargetGroup targets = (TargetGroup) ReadContext(context, Context.TARGETS + TargetKey);
            if (targets != null) {
                foreach (Character target in targets) {
                    context.Set(Context.CURRENT_TARGET, target);
					if (Filter.Filter(context))
                        Apply(target, context);
                }
            }
		}
		public abstract void Apply(Character target, Context context);
	}
}
