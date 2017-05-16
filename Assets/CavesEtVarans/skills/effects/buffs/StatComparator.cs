using System;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatComparator : ContextDependent, IStatModifier {
		public ModifierType Type { get; private set; }
		public IValueCalculator Value { get; set; }
		public ComparisonType Choice { get; set; }

		public double GetValue(double originalValue) {
			double val = Value.Value();
            return Choice == ComparisonType.Highest ? 
				Math.Max(val, originalValue) : 
				Math.Min(val, originalValue);
		}

		public StatComparator() {
			Type = ModifierType.Override;
		}
	}

	public enum ComparisonType {
		Highest, Lowest
	}
}
