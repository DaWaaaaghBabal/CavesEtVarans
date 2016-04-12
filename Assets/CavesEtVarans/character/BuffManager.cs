using CavesEtVarans.skills.effects;
using System.Collections.Generic;
using CavesEtVarans.skills.effects.buffs;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.character {
	public class BuffManager {
		private List<BuffInstance> buffs;
		private Character Character { set; get; }

		public BuffManager(Character character) {
			Character = character;
			buffs = new List<BuffInstance>();
		}

		public void ApplyBuff(BuffInstance buff, Context context) {
			buffs.Add(buff);
			buff.ApplyOn(Character);
		}

		public void Tick() {
			foreach (BuffInstance b in buffs) {
				b.Tick();
			}
		}

		public void RemoveBuff(BuffInstance buff) {
			buffs.Remove(buff);
			buff.RemoveFrom(Character);
		}
	}
}