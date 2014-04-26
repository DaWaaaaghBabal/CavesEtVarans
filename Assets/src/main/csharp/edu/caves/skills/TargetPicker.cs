using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class TargetPicker {

        private HashSet<Character> targets;
        private string key;
        private int targetNumber;

        public TargetPicker(int numberOfTargets, string targetKey) {
            targetNumber = numberOfTargets;
            key = targetKey;
        }

        public void AddTarget(Character character) {
            targets.Add(character);
            if (targets.Count == targetNumber) {
                EndPicking();
            }
        }

        private void EndPicking() {
            Context.Set(key, targets);
            ((Skill)Context.Get("skill")).NextTargetPicker();
        }

        public void Activate() {
            targets = new HashSet<Character>();
            TargetHandler.SetTargetPicker(this);
        }
    }
}
