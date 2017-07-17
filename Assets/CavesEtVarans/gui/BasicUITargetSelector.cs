using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using CavesEtVarans.skills.effects;
using System;
using System.Collections.Generic;

namespace CavesEtVarans.gui {
	public class BasicUITargetSelector : ITargetSelector {
		private Skill movementSkill;
		public BasicUITargetSelector() {
			movementSkill = new Skill();
            movementSkill.AddEffect(new OrientationEffect() {
                TargetKey = ContextKeys.MOVEMENT_TARGET,
                OrientationTargetKey = ContextKeys.TILE
            });
            movementSkill.AddEffect(new MovementEffect()
            {
                TargetKey = ContextKeys.MOVEMENT_TARGET,
                TileTargetKey = ContextKeys.TILE
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
				int jump = source.GetStatValue(Statistic.JUMP);
				if (Battlefield.AreNeighbours(sourceTile, targetTile)
				&& Math.Abs(sourceTile.Layer - targetTile.Layer) <= jump) {
					int movementCost = source.GetStatValue(Statistic.MOVEMENT_COST)
					    + sourceTile.GetMovementCost()
					    + targetTile.GetMovementCost();
					Cost cost = new Cost();
					cost.Add("AP", movementCost);
					movementSkill.Cost = cost;
                    Dictionary<string, object> skillData = new Dictionary<string, object>();
                    skillData[ContextKeys.SOURCE] = source;
                    skillData[ContextKeys.MOVEMENT_TARGET] = new TargetGroup(source);
                    skillData[ContextKeys.START_TILE] = new TargetGroup(sourceTile);
                    skillData[ContextKeys.TILE] = new TargetGroup(targetTile);

                    movementSkill.InitSkill(skillData);
				}
			}
		}

		public void Activate() {
			GUIEventHandler.Get().ActivePicker = this;
        }

        public void Cancel() {
            //Nothing to do.
        }

        public void Terminate() {
            //Nothing to do.
        }

        public void Activate(ICoordinates center, int centerheight, int suffix) {
            //Nothing to do.
        }
    }
}
