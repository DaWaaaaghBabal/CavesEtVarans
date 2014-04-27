using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public abstract class TargetPicker {

        protected HashSet<Character> targets;
        protected string key;

        public TargetPicker(string targetKey) {
            key = targetKey;
        }

        public abstract void AddTarget(Character character);

        protected void EndPicking() {
            Context.Set(key, targets);
            ((Skill)Context.Get("skill")).NextTargetPicker();
        }

        public void Activate() {
            targets = new HashSet<Character>();
            GUIEventHandler.SetTargetPicker(this);
        }

        public abstract void MouseOverCharacter(Character character);
    }
}
