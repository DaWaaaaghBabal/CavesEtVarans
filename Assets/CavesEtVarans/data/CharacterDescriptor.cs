using System.Collections.Generic;

namespace CavesEtVarans.data {
    public class CharacterDescriptor {
        public string Name { set; get; }
        public string Race { set; get; }
        public string Class { set; get; }
        public int Level { set; get; }
        public FactionDescriptor Faction{ set; get; }
        public Dictionary<string, int> StatIncreases { set; get; }
        public int[] StartingPosition { set; get; }
    }
}
