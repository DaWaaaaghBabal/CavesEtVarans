using CavesEtVarans.character;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.filters {
	public class FriendOrFoe : AbstractFilter {
		public character.factions.FriendOrFoe Accepts { set; get; }
        public string PointOfViewKey { set; get; }
        protected override bool FilterContext() {
			Character source = ReadContext(PointOfViewKey) as Character;
			Character trigger = ReadContext(TargetKey) as Character;
            character.factions.FriendOrFoe friendOrFoe = source.FriendOrFoe(trigger);
            return (friendOrFoe & Accepts) > 0;
        }
	}
}
