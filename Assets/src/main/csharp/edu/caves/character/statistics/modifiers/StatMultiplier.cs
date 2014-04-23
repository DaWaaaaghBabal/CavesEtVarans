using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavesEtVarans {
    class StatMultiplier : StatModifier{
        
        private double value;
        public StatMultiplier(double newValue) {
            value = newValue;
        }

        public override double getValue(double originalValue) {
            return value * originalValue;
        }

        public override void dispatch(Statistic stat) {
            stat.AddMultiplier(this);
        }
    }
}
