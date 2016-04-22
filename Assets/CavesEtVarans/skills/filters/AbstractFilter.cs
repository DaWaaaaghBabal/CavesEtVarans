using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
	public abstract class AbstractFilter : ContextDependent {
		public abstract bool Filter(Context c);
	}
}