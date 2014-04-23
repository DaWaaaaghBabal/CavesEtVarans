using System;
namespace CavesEtVarans
{
	public abstract class StatModifier {
		public abstract double getValue (double originalValue);

		public abstract void dispatch (Statistic stat);
	}
}

