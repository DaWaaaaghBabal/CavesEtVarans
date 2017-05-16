using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class CharacterInTargets : AbstractFilter {
        public string CharacterKey { set; get; }

        protected override bool FilterContext() {
            Character character = ReadContext(CharacterKey) as Character;
            TargetGroup targets = ReadContext(ContextKeys.TRIGGERING_TARGETS) as TargetGroup;
            return targets.Contains(character);
        }
    }
}
