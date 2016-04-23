using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
	public abstract class AbstractFilter : ContextDependent {
        private string targetKey;
        public string TargetKey {
            set {
                targetKey = value;
            }
            get {
                if (targetKey == null) {
                    targetKey = Context.FILTER_TARGET;
                }
                return targetKey;
            }
        }
        public abstract bool Filter(Context c);
	}
}