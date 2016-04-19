using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
    public class TargetGroup : HashSet<ITargetable> {
        public TargetGroup(ITargetable target) : base(){
            Add(target);
        }
        public TargetGroup() : base() { }

		public void Add(TargetGroup group) {
			foreach (ITargetable target in group) {
				Add(target);
			}
		}
	}
}