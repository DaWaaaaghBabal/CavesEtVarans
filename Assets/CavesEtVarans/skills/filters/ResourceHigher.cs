using CavesEtVarans.character;
using CavesEtVarans.character.resource;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public enum ValueOrPercentage {
        Value, Percentage
    }
    public class ResourceHigher : AbstractFilter {
        public string ResourceKey { set; get; }
        public ValueOrPercentage Type { set; get; }
        public IValueCalculator Threshold { set; get; }
        protected override bool FilterContext() {
            Character target = ReadContext(TargetKey) as Character;
            if (target == null)
                return true;
            Resource resource = target.GetResource(ResourceKey);
            double ratio = (Type == ValueOrPercentage.Value) ? resource.Value : resource.Percentage;
            double threshold = Threshold.Value();
            if (ratio == threshold) return !Inverted;
            return ratio > threshold;
        }
    }
}
