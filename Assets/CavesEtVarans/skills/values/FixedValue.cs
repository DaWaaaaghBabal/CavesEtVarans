using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values {
	public class FixedValue : ContextDependent, IValueCalculator {
		public double Val { set; get; }
		public double Value(Context context) {
			return Val;
		}
	}
}
