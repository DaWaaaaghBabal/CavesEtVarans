using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
    public class SameCharacterFilter : AbstractFilter{
        public string FirstCharacterKey { set; get; }
        public string SecondCharacterKey { set; get; }
        public override bool Filter(Context c) {
            Character c1 = ReadContext(c, FirstCharacterKey) as Character;
            Character c2 = ReadContext(c, SecondCharacterKey) as Character;
            return c1 == c2;
        }
    }
}
