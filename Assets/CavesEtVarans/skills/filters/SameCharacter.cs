using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class SameCharacter : AbstractFilter{
        public string ComparedCharacterKey { set; get; }
        protected override bool FilterContext() {
            Character c1 = ReadContext(ComparedCharacterKey) as Character;
            Character c2 = ReadContext(TargetKey) as Character;
            return c1 == c2;
        }
    }
}
