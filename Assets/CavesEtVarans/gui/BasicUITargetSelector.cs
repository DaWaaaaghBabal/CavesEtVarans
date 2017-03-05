using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.effects;
using System;
using CavesEtVarans.skills.targets;

namespace CavesEtVarans.gui {
	public class BasicUITargetSelector : ITargetSelector {
		private Skill movementSkill;
		public BasicUITargetSelector() {
			movementSkill = new Skill();
            movementSkill.AddTargetSelector(new TargetSelector()
            {
                TargetKey = Context.TILE,
            });
            movementSkill.AddEffect(new OrientationEffect() {
                TargetKey = Context.MOVEMENT_TARGET,
                OrientationTargetKey = Context.TILE
            });
            movementSkill.AddEffect(new MovementEffect()
            {
                TargetKey = Context.MOVEMENT_TARGET,
                TileTargetKey = Context.TILE
            });
			movementSkill.Attributes.Animation = "Walk";
            movementSkill.Flags[SkillFlag.NoEvent] = true;
        }

		public void TargetCharacter(Character character) {
			MainGUI.DisplayCharacter(character);
		}

		public void TargetTile(Tile targetTile) {
			if (targetTile.IsFree) {
				Character source = CharacterManager.Get().ActiveCharacter;
				Tile sourceTile = source.Tile;
				Context context = Context.ProvideMovementContext(source, targetTile);
				int jump = source.GetStatValue(Statistic.JUMP, context);
				if (Battlefield.AreNeighbours(sourceTile, targetTile)
				&& Math.Abs(sourceTile.Layer - targetTile.Layer) <= jump) {
					int movementCost = source.GetStatValue(Statistic.MOVEMENT_COST, context);
					movementCost += sourceTile.GetMovementCost(context);
					movementCost += targetTile.GetMovementCost(context);
					Cost cost = new Cost();
					cost.Add("AP", movementCost);
					movementSkill.Cost = cost;
					movementSkill.UseSkill(context);
				}
			}
		}

		public void Activate(Context context) {
			GUIEventHandler.Get().ActivePicker = this;
		}

        public void Cancel() {
            // Nothing to do.
        }
    }
}
