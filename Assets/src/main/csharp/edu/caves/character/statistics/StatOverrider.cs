using System;

namespace CavesEtVarans
{
	public class StatOverrider : StatModifier {
		public StatOverrider () {

		}

        
        override public double getValue(double originalValue)
        {
            return originalValue;
        }

	}
}

