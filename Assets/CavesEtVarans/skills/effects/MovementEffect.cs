using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;
using System.Collections.Generic;

namespace CavesEtVarans.skills.effects {
	public class MovementEffect : TargetedEffect {

        public string TileTargetKey { set; get; }

		public override void Apply(Character target, Context context) {
            HashSet<ITargetable> tiles = (HashSet < ITargetable > )ReadContext(context, Context.TARGETS + TileTargetKey);
            // There should be only one target tile, but it's still a set, so we take the first element that comes
            Tile[] tileArray = new Tile[tiles.Count];
            tiles.CopyTo(tileArray);
            Tile targetTile = tileArray[0];
            Tile startTile = target.Tile;
            Battlefield.Move(target, targetTile);
            new MovementEvent(target, startTile, targetTile).Trigger(context);
		}
	}
}