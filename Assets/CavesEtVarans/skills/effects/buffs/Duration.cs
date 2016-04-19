using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public abstract class Duration : ContextDependent {
		public abstract Duration Instantiate(Context context);
		public abstract int Tick();
	}

	public class Countdown : Duration {
		public int Duration { set; get; }
		public override Duration Instantiate(Context context) {
			return MemberwiseClone() as Duration;
		}

		public override int Tick() {
			return --Duration;
		}
	}
}