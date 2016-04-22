using CavesEtVarans.character;
using CavesEtVarans.character.factions;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.triggers.filters {
	public class FriendOrFoeFilter : TriggerFilter {
		public FriendOrFoe Accepts { set; get; }

		public override bool Filter(Context c) {
			Character source = ReadContext(c, Context.SOURCE) as Character;
			Character trigger = ReadContext(c, Context.TRIGGERING_CHARACTER) as Character;
			FriendOrFoe friendOrFoe = source.FriendOrFoe(trigger);
			return (friendOrFoe & Accepts) > 0;
		}
	}
}
