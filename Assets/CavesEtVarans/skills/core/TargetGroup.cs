using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
    public class TargetGroup : HashSet<ITargetable> {
        public TargetGroup(ITargetable target) : base(){
            Add(target);
        }
        public TargetGroup() : base() { }
    }
}