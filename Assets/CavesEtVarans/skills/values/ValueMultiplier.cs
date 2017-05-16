using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values {
	public class ValueMultiplier : ContextDependent, IValueCalculator {
		public IValueCalculator Base { set; get; }
		public IValueCalculator Factor { set; get; }
		public double Value() {
			return Base.Value() * Factor.Value();
		}
	}
}
