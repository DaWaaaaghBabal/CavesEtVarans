using System;
using System.Collections.Generic;
namespace CavesEtVarans
{
	public abstract class StatModifier {
        public abstract double GetValue(double originalValue);

		public abstract void dispatch (Statistic stat);

    }
}

