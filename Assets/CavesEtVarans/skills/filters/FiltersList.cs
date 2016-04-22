using System.Collections.Generic;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class FiltersList : List<AbstractFilter> {
        public bool Filter(Context c) {
            foreach (AbstractFilter filter in this)
                if (!filter.Filter(c))
                    return false;
            return true;
        }
    }
}
