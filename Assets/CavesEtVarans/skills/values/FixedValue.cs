using System;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values {
	public class FixedValue : IValueCalculator {
		public double Val { get; set; }
		public double Value(Context context) {
			return Val;
		}
	}
}
