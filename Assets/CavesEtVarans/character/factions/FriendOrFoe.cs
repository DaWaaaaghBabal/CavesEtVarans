using System;

namespace CavesEtVarans.character.factions {
	[Flags]
	public enum FriendOrFoe {
		Ally = 1, Friend = 2, Neutral = 4, Foe = 8
	}
}