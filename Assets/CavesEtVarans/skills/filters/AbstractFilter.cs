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
        public bool Inverted { set; get; }
        public bool Filter (Context c) {
            return Inverted ^ FilterContext(c);
        }
        protected abstract bool FilterContext(Context c);
	}
}