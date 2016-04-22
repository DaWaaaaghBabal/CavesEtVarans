using CavesEtVarans.character;
using CavesEtVarans.character.factions;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
	public class FriendOrFoeFilter : AbstractFilter {
		public FriendOrFoe Accepts { set; get; }
        public string PointOfViewKey { set; get; }
        public string TargetKey { set; get; }
        public override bool Filter(Context c) {
			Character source = ReadContext(c, PointOfViewKey) as Character;
			Character trigger = ReadContext(c, TargetKey) as Character;
            FriendOrFoe friendOrFoe = source.FriendOrFoe(trigger);
            return (friendOrFoe & Accepts) > 0;
        }
	}
}
