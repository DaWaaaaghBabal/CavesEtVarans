using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class OrientationEffect : TargetedEffect {
        public string OrientationTargetKey { get; set; }

        public override void Apply(Character target) {
            TargetGroup targets = (TargetGroup)ReadContext(OrientationTargetKey);
            // We need to average the positions of all targets to know where to look.
            int row = 0, column = 0, layer = 0, count = targets.Count;
            foreach(ITargetable tgt in targets) {
                row += tgt.Tile.Row;
                column += tgt.Tile.Column;
                layer += tgt.Tile.Layer;
            }
            Tile targetTile = new Tile(row / count, column / count, layer / count, 0);
            Tile currentTile = target.Tile;
            Orientation newOrientation = Battlefield.Direction(currentTile, targetTile);
			Orientation oldOrientation = target.Orientation;
			target.Orientation = newOrientation;
            if (!newOrientation.Equals(oldOrientation))
			    new OrientationEvent(target, oldOrientation, newOrientation).Trigger();
		}
	}
}