using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavesEtVarans {
    class Gauge {
        private int min;
        private int max;
        private int value;

        public Gauge(int newMin, int newMax) {
            min = newMin;
            max = newMax;
            value = min;
        }

        public int GetValue() {
            return value;
        }

        public void SetValue(int newValue) {
            if (newValue < min) {
                value = min;
            } else if (newValue > max) {
                value = max;
            } else {
                value = newValue;
            }
        }

        public double getPercentage() {
            return (value - min) / (max - min);
        }
    }
}
