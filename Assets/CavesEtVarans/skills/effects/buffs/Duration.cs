using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public abstract class Duration : ContextDependent {
        public virtual Duration Instantiate(Context context) {
            return MemberwiseClone() as Duration;
        }
		public abstract int HalfTick();
	}

	public class Countdown : Duration {
		public double Duration { set; get; }
		public override int HalfTick() {
			Duration -= 0.5;
            return (int) Math.Ceiling(Duration); 
		}
	}
    public class Constant : Duration {
        public override int HalfTick() {
            return 1;
        }
    }
}