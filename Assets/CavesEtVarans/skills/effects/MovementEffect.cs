﻿using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.events;

namespace CavesEtVarans.skills.effects {
	public class MovementEffect : TargetedEffect {

        public string TileTargetKey { set; get; }

		public override void Apply(Character target, Context context) {
            TargetGroup tiles = (TargetGroup)ReadContext(context, Context.TARGETS + TileTargetKey);
            Tile targetTile = tiles.PickOne as Tile;
            Tile startTile = target.Tile;
            Battlefield.Move(target, targetTile);
            new MovementEvent(target, startTile, targetTile).Trigger(context);
		}
	}
}