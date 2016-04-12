using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values
{
	public class DamageFormula : IValueCalculator
	{
		public IValueCalculator Attack { set; get; }
		public IValueCalculator Defense { set; get; }

		public double Value(Context context)
		{
			double diff = Attack.Value(context) - Defense.Value(context);
			double sum = Attack.Value(context) + Defense.Value(context);
			return 1.0 + 0.5 * Math.Tanh(diff / sum);
		}
	}
}
