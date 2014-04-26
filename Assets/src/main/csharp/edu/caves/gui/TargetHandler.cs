using System;
using System.Collections.Generic;

namespace CavesEtVarans {
    public class TargetHandler {

        public static void HandleClickOnTile(Tile tile) {

        }

        public static void HandleClickOnCharacter(SceneCharacter sceneCharacter) {
            activePicker.AddTarget(sceneCharacter.GetCharacter());
        }

        private static TargetPicker activePicker;
        public static void SetTargetPicker(TargetPicker targetPicker) {
            activePicker = targetPicker;
        }
    }
}
