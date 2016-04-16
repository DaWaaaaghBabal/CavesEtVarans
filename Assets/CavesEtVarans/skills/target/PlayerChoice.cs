using System;
using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.skills.core;
using System.Collections.Generic;
using CavesEtVarans.graphics;

namespace CavesEtVarans.skills.target {
	public class PlayerChoice : PlayerChoiceStrategy {
		private HashSet<Tile> area;
		private int radius;
		public PlayerChoice(TargetPickerCallback callback, FlagsList<TargetFlag> flags) : base(callback, flags) {
		}

		public override void Activate(TargetPicker targetPicker, Context context) {
			Character source = (Character) ReadContext(context, targetPicker.SourceKey);
			radius = targetPicker.AoeRadius;
			area = GetArea(source.Tile, targetPicker.Range);
			//@TODO decouple from graphics (use events);
			GraphicBattlefield.HighlightArea(area);
		}

		public override PlayerChoiceType ChoiceType() {
			return PlayerChoiceType.PlayerChoice;
		}

		public override bool TargetCharacter(Character character) {
			return TargetTile(character.Tile);
		}

		public override bool TargetTile(Tile tile) {
			if (area.Contains(tile)) {
				bool result = false;
				foreach (Tile t in GetArea(tile, radius))
					result |= Callback(t);
				return result;
			}
			return false;
		}
	}
}