using System;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatComparator : ContextDependent, IStatModifier {
		public ModifierType Type { get; private set; }
		public IValueCalculator Value { get; set; }
		public ComparisonType Choice { get; set; }

		public double GetValue(double originalValue, Context context) {
			bool highest = Choice == ComparisonType.Highest;
			double val = Value.Value(context);
			return highest ? 
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
