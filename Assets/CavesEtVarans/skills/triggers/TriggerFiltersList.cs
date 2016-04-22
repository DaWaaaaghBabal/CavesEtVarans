using System.Collections.Generic;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.filters;

namespace CavesEtVarans.skills.triggers {
	public class TriggerFiltersList : List<AbstractFilter> {
		public bool Filter(Context c) {
			foreach (AbstractFilter filter in this) {
				if (!filter.Filter(c)) return false;
			}
			return true;
		}
	}
}