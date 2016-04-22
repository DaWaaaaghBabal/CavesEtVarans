using System.Collections.Generic;

namespace CavesEtVarans.character.factions {
	public class Faction {

		private Dictionary<Faction, FriendOrFoe> friendsAndFoes;
		public string Name { set; get; }

		public Faction(string name) {
			Name = name;
			friendsAndFoes = new Dictionary<Faction, FriendOrFoe>();
		}

		public FriendOrFoe FriendOrFoe(Faction other) {
			if (other == this) return factions.FriendOrFoe.Friend;
			if(friendsAndFoes.ContainsKey(other))
				return friendsAndFoes[other];
			return factions.FriendOrFoe.Neutral;
		}

		public void TreatAs(Faction other, FriendOrFoe friendOrFoe) {
			friendsAndFoes[other] = friendOrFoe;
		}
		
	}
}
