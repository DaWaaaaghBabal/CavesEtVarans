using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
    public interface IContextDependent {
        ContextDependent Parent { get; set; }
        ContextDependent Clone();
        void SetLocalContext(Dictionary<string, object> contextData);
        void SetLocalContext(string key, object value);
    }
}