using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class DiscreteTargetPicker : TargetPicker{
        protected int targetNumber;

        public DiscreteTargetPicker(int numberOfTargets, string targetKey) : base(targetKey) {
            targetNumber = numberOfTargets;
        }

        public override void AddTarget(Character character) {
            targets.Add(character);
            if (targets.Count == targetNumber) {
                EndPicking();
            }
        }

        public override void MouseOverCharacter(Character character) {
            MainGUI.DisplayCharacter(character);
        }

        public override void ClickOnTile(Tile tile) {
            MainGUI.GetInstance().DisplayTileCoordinates(tile);
        }
    }
}
