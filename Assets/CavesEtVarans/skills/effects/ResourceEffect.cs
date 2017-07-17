using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.effects {
    public class ResourceEffect : TargetedEffect {
        public IValueCalculator Amount { set; get;}
        public string ResourceKey { set; get; }
        public override EffectResult Apply(Character target, int offset) {
            int amount = target.IncrementResource(ResourceKey, (int)Amount.Value());
            return new EffectResult(target, amount);
        }

        public override EffectResult Apply(Tile tile, int suffix) {
            throw new NotImplementedException("Trying to give resources to a tile ! Please check YAML for inconsistencies.");
        }
    }
}
