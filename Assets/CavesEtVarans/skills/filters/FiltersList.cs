using System.Collections.Generic;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class FiltersList : List<AbstractFilter> {
        public bool Filter(Context c) {
            bool result = true;
            foreach (AbstractFilter filter in this)
                result &= filter.Filter(c);
            return result;
        }
    }
}
