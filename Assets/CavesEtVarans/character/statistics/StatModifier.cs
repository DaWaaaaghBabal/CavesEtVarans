using CavesEtVarans.skills.core;
using System;

namespace CavesEtVarans.character.statistics {
	public interface IStatModifier {
		// Given a value and a Context, returns the modified value. 
		// For example, an effect that increases a stat by 25% would return originalValue * 1.25.
		double GetValue(double originalValue, Context context);

		/* Effects that multiply a stat (or % increases) must use ModifierPriority.Multiply
		 * Effects that increase or decrease a stat by a flat value must use ModifierPriority.Increment
		 * Effects that replace a stat with another value must use ModifierPriority.Override
		 * Otherwise, differences in application order will lead to inconsistent results.
		 */
		ModifierPriority Priority { set; get; }
		IValueCalculator value { set; get; }
	}
	
	public enum ModifierPriority {
		Multiply, Increment, Override
	}
}
