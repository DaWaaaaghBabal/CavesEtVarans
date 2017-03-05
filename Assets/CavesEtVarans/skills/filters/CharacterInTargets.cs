using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class CharacterInTargets : AbstractFilter {
        public string CharacterKey { set; get; }

        protected override bool FilterContext(Context c) {
            Character character = ReadContext(c, CharacterKey) as Character;
            TargetGroup targets = ReadContext(c, Context.TRIGGERING_TARGETS) as TargetGroup;
            return targets.Contains(character);
        }
    }
}
