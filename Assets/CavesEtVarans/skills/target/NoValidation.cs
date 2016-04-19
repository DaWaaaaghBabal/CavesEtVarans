using CavesEtVarans.battlefield;
using CavesEtVarans.character;
using CavesEtVarans.exceptions;
using CavesEtVarans.skills.core;

namespace CavesEtVarans.skills.target {

	public class NoValidation : PlayerChoiceStrategy {
		public NoValidation(TargetPickerCallback callback, FlagsList<TargetFlag> flags) : base(callback, flags) {}

		public override void Activate(TargetPicker targetPicker, Context context) {
            // Nothing is asked from the player. Which means we already know who will be targeted.
            Character source = (Character)ReadContext(context, targetPicker.SourceKey);
            foreach (Tile t in GetArea(source.Tile, targetPicker.MinRange, targetPicker.MaxRange)) {
                Callback(t);
            }
            targetPicker.EndPicking();
        }

        public override PlayerChoiceType ChoiceType() {
			return PlayerChoiceType.NoValidation;
		}

		public override bool TargetCharacter(Character character) {
			throw new TargetPickerExecutionException("A no-validation target picker should never have to call TargetCharacter");
		}

		public override bool TargetTile(Tile tile) {
			throw new TargetPickerExecutionException("A no-validation target picker should never have to call TargetTile");
		}
	}
}