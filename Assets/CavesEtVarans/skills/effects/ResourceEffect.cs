using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects {
    public class ResourceEffect : TargetedEffect {
        public IValueCalculator Amount { set; get;}
        public string ResourceKey { set; get; }
        public override void Apply(Character target, Context context) {
            int amount = (int) Amount.Value(context);
            target.IncrementResource(ResourceKey, amount);
        }
    }
}
