using System;
using System.Collections.Generic;
namespace CavesEtVarans
{
	public abstract class StatModifier {
        public abstract double getValue(double originalValue);

		public abstract void dispatch (Statistic stat);


        public StatModifier() {
            
        }
    }
}

