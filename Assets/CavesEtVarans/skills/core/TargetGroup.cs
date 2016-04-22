using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
    public class TargetGroup : HashSet<ITargetable> {
        public TargetGroup() {
        }

        public TargetGroup(ITargetable target) : base(){
            Add(target);
            PickOne = target;
        }

        public ITargetable PickOne { get; private set; }

        public void Add(TargetGroup group) {
			foreach (ITargetable target in group) {
				Add(target);
			}
            PickOne = group.PickOne;
		}
	}
}