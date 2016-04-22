using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
    public class TargetGroup : HashSet<ITargetable> {
        public TargetGroup() {
        }

        public TargetGroup(ITargetable target) : base(){
            Add(target);
        }

		public void Add(TargetGroup group) {
			foreach (ITargetable target in group) {
				Add(target);
			}
		}
	}
}