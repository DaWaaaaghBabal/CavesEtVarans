using CavesEtVarans.character;
using CavesEtVarans.skills.core;
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

		public void Apply() {
			object targets = ReadContext(TargetKey);
            if (targets.GetType().Equals(typeof(Character)))
                ApplyOn(targets as Character);
            else if (targets != null) {
                foreach (Character target in targets as TargetGroup) {
                    ApplyOn(target);
                }
            }
		}

        private void ApplyOn(Character target) {
            SetContext(ContextKeys.CURRENT_TARGET, target);
            if (Filter.Filter())
                Apply(target);
        }

        public abstract void Apply(Character target);
	}
}
