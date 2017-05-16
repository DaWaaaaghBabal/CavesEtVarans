using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatIncrementer : ContextDependent, IStatModifier {
		public ModifierType Type { get; private set; }
		public IValueCalculator Value { get; set; }

		public double GetValue(double originalValue) {
			return originalValue + Value.Value();
		}

		public StatIncrementer() {
			Type = ModifierType.Increment;
		}
	}
}
