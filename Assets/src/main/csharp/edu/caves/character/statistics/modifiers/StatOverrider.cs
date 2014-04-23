using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavesEtVarans {
    class StatOverrider : StatModifier{

        private double value;
        public StatOverrider(double newValue) {
            value = newValue;
        }

        public override double getValue(double originalValue) {
            return value;
        }

        public override void dispatch(Statistic stat) {
            stat.AddOverrider(this);
        }
    }
}
