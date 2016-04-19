using System.Collections.Generic;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.triggers {
	public class TriggerFiltersList : List<TriggerFilter> {
		public bool Filter(Context c) {
			foreach (TriggerFilter filter in this) {
				if (!filter.Filter(c)) return false;
			}
			return true;
		}
	}
}