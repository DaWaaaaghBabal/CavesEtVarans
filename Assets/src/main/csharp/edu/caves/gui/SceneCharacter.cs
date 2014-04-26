using System;
using System.Collections.Generic;
using UnityEngine;

namespace CavesEtVarans {
    class SceneCharacter : MonoBehaviour {

        // A SceneCharacter is the representation in the Unity world of a Character in the game logic.
        private Character character;
        public Character GetCharacter() {
            return character;
        }
        public void SetCharacter(Character newCharacter) {
            character = newCharacter;
        }

        void OnClick() {
            TargetHandler.HandleClickOnCharacter(this);
        }
    }
}
