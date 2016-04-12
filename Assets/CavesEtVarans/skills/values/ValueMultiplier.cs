using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values {
	public class ValueMultiplier : IValueCalculator {
		public IValueCalculator Base { set; get; }
		public IValueCalculator Factor { set; get; }
		public double Value(Context context) {
			return Base.Value(context) * Factor.Value(context);
		}
	}
}
