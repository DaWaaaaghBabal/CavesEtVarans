using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.triggers {
	public abstract class TriggerFilter : ContextDependent {
		public abstract bool Filter(Context c);
	}
}