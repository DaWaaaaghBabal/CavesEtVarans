using System;
using System.Collections.Generic;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class FiltersList : List<AbstractFilter>, IContextDependent {
        public ContextDependent Parent { get; set; }

        public ContextDependent Clone() {
            return MemberwiseClone() as ContextDependent;
        }

        public bool Filter() {
            bool result = true;
            foreach (AbstractFilter filter in this)
                result &= filter.Filter();
            return result;
        }

        public void SetLocalContext(Dictionary<string, object> contextData) {
            foreach (AbstractFilter f in this)
                f.SetLocalContext(contextData);
        }

        public void SetLocalContext(string key, object value) {
            foreach (AbstractFilter f in this)
                f.SetLocalContext(key, value);
        }
    }
}
