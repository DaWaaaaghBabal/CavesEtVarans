using CavesEtVarans.battlefield;
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
        public string ResultKey { set; get; }

        public void Apply() {
            ITargetable target = ReadContext(TargetKey) as ITargetable;
            EffectResult result = target.DispatchEffect(this, 0);
            if (ResultKey != null) SetContext(ResultKey, result);
        }

        public abstract EffectResult Apply(Character character, int suffix);
        public abstract EffectResult Apply(Tile tile, int suffix);
    }
}
