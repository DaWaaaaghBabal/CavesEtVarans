using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public abstract class BuffInstance : ContextDependent {
		public Duration Duration { set; get; }

		public void Tick() {
			if (Duration.Tick() == 0) {
				Character target = (Character) ReadContext(null, Context.BUFF_TARGET);
				RemoveFrom(target);
			    target.RemoveBuff(this);
			}
		}

        public abstract void ApplyOn(Character target);
        public abstract void RemoveFrom(Character target);
	}
}
