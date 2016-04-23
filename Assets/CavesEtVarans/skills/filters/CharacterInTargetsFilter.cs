using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class CharacterInTargetsFilter : AbstractFilter {
        public string CharacterKey { set; get; }

        public override bool Filter(Context c) {
            Character character = ReadContext(c, CharacterKey) as Character;
            TargetGroup targets = ReadContext(c, Context.TRIGGERING_TARGETS) as TargetGroup;
            return targets.Contains(character);
        }
    }
}
