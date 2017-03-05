using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects.buffs {
	public class StatMultiplier : StatModifier {
        [YamlDotNet.Serialization.YamlIgnore]
        public override ModifierType Type {
            get { return ModifierType.Multiply; }
            protected set { }
        }
        protected override double GetModifiedValue(double originalValue, Context context) {
            Context localContext = CopyContext(context);
            return originalValue * Value.Value(localContext);
        }
    }
}
