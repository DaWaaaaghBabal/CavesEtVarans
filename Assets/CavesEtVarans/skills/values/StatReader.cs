using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.values
{
	public class StatReader : ContextDependent, IValueCalculator {
		public string Source { get; set; }
		public string Stat { get; set; }

		public  double Value(Context context) {
			Character character = (Character) (Character) ReadContext(context, Source);
			return character.GetStatValue(Stat, context);
		}
	}
}