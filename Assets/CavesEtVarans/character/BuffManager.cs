using CavesEtVarans.skills.effects.buffs;
using System.Collections.Generic;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.effects;

namespace CavesEtVarans.character {
	public class BuffManager {
		private List<BuffInstance> buffs;
		private Character Character { set; get; }

		private IEnumerable<BuffInstance> TempCopy {
			set { }
			get {
				List<BuffInstance> copy = new List<BuffInstance> ();
				copy.AddRange(buffs);
				return copy;
			}
		}

		public BuffManager(Character character) {
			Character = character;
			buffs = new List<BuffInstance>();
		}

		public void ApplyBuff(BuffInstance buff) {
			bool alreadyThere = false;
			foreach (BuffInstance b in TempCopy) {
				alreadyThere = true;
				ApplyBuffEffect sourceEffect = b.SourceEffect;
				if (sourceEffect == buff.SourceEffect) {
					StackingStrategy stacking = sourceEffect.Stacking;
					stacking.Stack(b, buff, Apply, RemoveBuff);
				}
				break;
			}
			if (!alreadyThere) Apply(buff);
		}

		public void HalfTick() {
			foreach (BuffInstance b in TempCopy) {
				b.Tick();
			}
		}
		
		private void Apply(BuffInstance buff) {
			buffs.Add(buff);
			buff.ApplyOn(Character);
		}

		public void RemoveBuff(BuffInstance buff) {
			buff.RemoveFrom(Character);
			buffs.Remove(buff);
		}
	}
}