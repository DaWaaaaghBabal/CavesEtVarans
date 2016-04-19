using CavesEtVarans.skills.core;
using System;

namespace CavesEtVarans.character.statistics {
	public interface IStatModifier {
		// Given a value and a Context, returns the modified value. 
		// For example, an effect that increases a stat by 25% would return originalValue * 1.25.
		double GetValue(double originalValue, Context context);

		/* Effects that multiply a stat (or % increases) must use ModifierType.Multiply
		 * Effects that increase or decrease a stat by a flat value must use ModifierType.Increment
		 * Effects that replace a stat with another value must use ModifierType.Override
		 * Otherwise, differences in application order and lack of operation priority 
         * will lead to inconsistent results.
		 */
		ModifierType Type { get; }
		IValueCalculator Value { set; get; }
	}
	
	public enum ModifierType {
		Multiply, Increment, Override
	}
}
