using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    class StatMultiplier : StatModifier{
        
        private double value;
        public StatMultiplier(double newValue) {
            value = newValue;
        }

        public override double GetValue(double originalValue) {
            return value * originalValue;
        }

        public override void dispatch(Statistic stat) {
            stat.AddMultiplier(this);
        }
    }
}
