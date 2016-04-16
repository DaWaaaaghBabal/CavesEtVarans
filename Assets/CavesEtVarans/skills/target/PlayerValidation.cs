using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.graphics;
using CavesEtVarans.skills.core;
using System.Collections.Generic;

namespace CavesEtVarans.skills.target {

	public class PlayerValidation : PlayerChoiceStrategy {
		private HashSet<Tile> area;

		public PlayerValidation(TargetPickerCallback callback, FlagsList<TargetFlag> flags) : base(callback, flags) { }
        public override void Activate(TargetPicker targetPicker, Context context) {
            Character source = (Character) ReadContext(context, targetPicker.SourceKey);
            area = GetArea(source.Tile, targetPicker.AoeRadius);
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
			bool result = false;
            if (area.Contains(tile)) {
				foreach (Tile t in area) {
					result |= Callback(tile);
				}
			}
			return result;
		}
	}
}