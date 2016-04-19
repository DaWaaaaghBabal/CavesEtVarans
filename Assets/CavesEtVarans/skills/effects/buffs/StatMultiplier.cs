using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatMultiplier : IStatModifier {
		public ModifierType Type { get; private set; }
		public IValueCalculator Value { get; set; }
		
		public double GetValue(double originalValue, Context context) {
			return originalValue * Value.Value(context);
		}

		public StatMultiplier() {
			Type = ModifierType.Multiply;
		}
	}
}
