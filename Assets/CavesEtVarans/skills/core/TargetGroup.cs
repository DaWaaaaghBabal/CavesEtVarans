using System.Collections.Generic;

namespace CavesEtVarans.skills.core {
    public class TargetGroup : HashSet<ITargetable> {
        public ITargetable PickOne { get; private set; }
        public TargetGroup() {
        }

        public TargetGroup(ITargetable target) : base(){
            Add(target);
            PickOne = target;
        }
        new public void Add(ITargetable target) {
            base.Add(target);
            PickOne = target;
        }

        public void Add(TargetGroup group) {
			foreach (ITargetable target in group) {
				Add(target);
			}
            PickOne = group.PickOne;
		}
	}
}