using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class OrientationEffect : TargetedEffect {
        public string OrientationTargetKey { get; set; }

        public override EffectResult Apply(Character target, int suffix) {
            TargetGroup targets = (TargetGroup)ReadContext(suffix == 0 ? OrientationTargetKey : OrientationTargetKey + suffix);
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
            return EffectResult.Void;
		}

        public override EffectResult Apply(Tile tile, int suffix) {
            throw new NotImplementedException("Trying to orient a tile ! Please check YAML for inconsistencies.");
        }
    }
}