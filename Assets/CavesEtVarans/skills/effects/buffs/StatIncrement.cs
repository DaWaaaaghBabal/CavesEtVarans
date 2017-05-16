using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatIncrement : StatModifier {
        [YamlDotNet.Serialization.YamlIgnore]
        public override ModifierType Type {
			get { return ModifierType.Increment; }
			protected set { }
		}
		protected override double GetModifiedValue(double originalValue) {
			return originalValue + Value.Value();
		}
	}
}
