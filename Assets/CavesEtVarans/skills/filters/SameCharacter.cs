using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class SameCharacter : AbstractFilter{
        public string ComparedCharacterKey { set; get; }
        protected override bool FilterContext(Context c) {
            Character c1 = ReadContext(c, ComparedCharacterKey) as Character;
            Character c2 = ReadContext(c, TargetKey) as Character;
            return c1 == c2;
        }
    }
}
