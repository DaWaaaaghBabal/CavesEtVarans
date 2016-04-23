using System.Collections.Generic;
using CavesEtVarans.character.factions;

namespace CavesEtVarans.data {
    public class FactionDescriptor {
        public string Name { set; get; }
        public string Color { set; get; }
        public Dictionary<FactionDescriptor, FriendOrFoe> FriendsAndFoes { set; get; }
    }
}
