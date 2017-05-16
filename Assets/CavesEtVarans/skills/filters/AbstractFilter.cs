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
                    targetKey = ContextKeys.FILTER_TARGET;
                }
                return targetKey;
            }
        }
        public bool Inverted { set; get; }
        public bool Filter () {
            return Inverted ^ FilterContext();
        }
        protected abstract bool FilterContext();
	}
}