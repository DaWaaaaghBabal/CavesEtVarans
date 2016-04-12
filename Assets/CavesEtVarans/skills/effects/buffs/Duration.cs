using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public abstract class Duration {
		public abstract Duration Instantiate(Context context);
		public abstract int Tick();
	}
}