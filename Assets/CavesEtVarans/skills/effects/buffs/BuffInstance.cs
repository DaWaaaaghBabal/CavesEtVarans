using System.Collections.Generic;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class BuffInstance : ContextDependent {

		public ApplyBuffEffect SourceEffect { set; get; }
		public Duration Duration { set; get; }
		public int Stacks { set; get; }
		private HashSet<BuffEffect> effects;

		public BuffInstance() {
			effects = new HashSet<BuffEffect>();
			Stacks = 1;
		}

		public void AddEffect(BuffEffect effectinstance) {
			effects.Add(effectinstance);
		}

		public void Tick() {
			if (Duration.HalfTick() == 0) {
				Character target = ReadContext(Context.Empty, Context.BUFF_TARGET) as Character;
				RemoveFrom(target);
			}
		}

		public void ApplyOn(Character character, Context context) {
			foreach(BuffEffect effect in effects) {
				effect.ApplyOn(character, context);
			}
		}

		public void RemoveFrom(Character character) {
			foreach (BuffEffect effect in effects) {
				effect.RemoveFrom(character);
			}
		}
	}
}