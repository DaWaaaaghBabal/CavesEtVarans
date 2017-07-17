using System;
using System.Collections;
using System.Collections.Generic;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;

namespace CavesEtVarans.skills.core {
    public class TargetGroup : ContextDependent, ITargetable, IEnumerable<ITargetable> {
        public int Size {
            get {throw new NotImplementedException("The size of a TargetGroup should never be relevant. Check YAML for inconsistencies"); }
            set {throw new NotImplementedException("The size of a TargetGroup should never be relevant. Check YAML for inconsistencies"); }
        }

        public Tile Tile {                                                                                                                       
            get {throw new NotImplementedException("The tile occupied by a TargetGroup should never be relevant. Check YAML for inconsistencies");}
            set {throw new NotImplementedException("The tile occupied by a TargetGroup should never be relevant. Check YAML for inconsistencies");}
        }

        private List<ITargetable> targets;
        public ITargetable this[int index] {
            get { return targets[index]; }
            private set { targets[index] = value; }
        }
        public TargetGroup() {
            targets = new List<ITargetable>();
        }
        public TargetGroup(ITargetable target) : this(){
            Add(target);
        }

        public void Add(ITargetable target) {
            if (target is TargetGroup) {
                foreach (ITargetable t in (TargetGroup) target) {
                    targets.Add(t);
                }
            } else
                targets.Add(target);
        }

        public EffectResult DispatchEffect(IEffect effect, int suffix) {
            EffectResult result = new EffectResult();
            foreach (ITargetable target in this) {
                SetContext(ContextKeys.CURRENT_TARGET, target);
                result += target.DispatchEffect(effect, suffix);
                suffix++;
            }
            return result;
        }

        public void DispatchActivation(ITargetSelector selector, int suffix) {
            if (suffix < targets.Count) {
                this[suffix].DispatchActivation(selector, suffix);
            }
        }

        public void DispatchTermination(ITargetSelector selector, int suffix) {
            if (suffix < targets.Count - 1) {
                suffix++;
                DispatchActivation(selector, suffix);
            } else {
                selector.Terminate();
            }
        }

        public IEnumerator<ITargetable> GetEnumerator() {
            return ((IEnumerable<ITargetable>)targets).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<ITargetable>)targets).GetEnumerator();
        }

        public int Count {
            get { return targets.Count; }
            private set { }
        }

        public bool Contains(Character character) {
            return targets.Contains(character);
        }
    }
}