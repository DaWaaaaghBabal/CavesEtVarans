using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class MovementEffect : TargetedEffect {

        public string TileTargetKey { set; get; }

		public override EffectResult Apply(Character target, int suffix) {
            TargetGroup targetGroup = ReadContext(suffix == 0 ? TileTargetKey : TileTargetKey + suffix) as TargetGroup;
            Tile targetTile = targetGroup[0] as Tile;
            Tile startTile = target.Tile;
            Battlefield.Move(target, targetTile);
            new MovementEvent(target, startTile, targetTile).Trigger();
            return EffectResult.Void;
		}

        public override EffectResult Apply(Tile tile, int suffix) {
            throw new NotImplementedException("Trying to move a tile ! Please check YAML for inconsistencies.");
        }
    }
}