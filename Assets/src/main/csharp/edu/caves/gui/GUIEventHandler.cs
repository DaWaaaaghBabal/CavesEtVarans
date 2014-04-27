using System;
using System.Collections.Generic;
using UnityEngine;

namespace CavesEtVarans {
    public class GUIEventHandler {


        private static TargetPicker activePicker;
        public static void SetTargetPicker(TargetPicker targetPicker) {
            activePicker = targetPicker;
        }

        private static TargetPicker GetActivePicker() {
            if (activePicker == null) {
                activePicker = new BasicUITargetPicker("UItarget");
            }
            return activePicker;
        }

        public static void HandleClickOnTile(SceneTile tile) {

        }

        public static void HandleClickOnCharacter(SceneCharacter sceneCharacter) {
            GetActivePicker().AddTarget(sceneCharacter.GetCharacter());
        }

        public static void HandleMouseEnterCharacter(SceneCharacter sceneCharacter) {
            GetActivePicker().MouseOverCharacter(sceneCharacter.GetCharacter());
        }

        public static void HandleMouseExitCharacter(SceneCharacter sceneCharacter) {
            GetActivePicker().MouseOverCharacter(null);
        }
    }
}
