using System;
using System.Collections.Generic;
using UnityEngine;

namespace CavesEtVarans {
    public class SceneCharacter : MonoBehaviour {

        /* A SceneCharacter is the representation in the Unity world of a Character in the game logic. 
         * Basically, it's only a visual representation that will manage clicks, animation, position and stuff
         * but no game logic.
         */
        
        private Character character;
        public Character GetCharacter() {
            return character;
        }
        public void SetCharacter(Character newCharacter) {
            character = newCharacter;
        }

        void OnMouseDown() {
            GUIEventHandler.HandleClickOnCharacter(this);
        }

        void OnMouseEnter() {
            GUIEventHandler.HandleMouseEnterCharacter(this);
        }

        void OnMouseExit() {
            GUIEventHandler.HandleMouseExitCharacter(this);
        }
    }
}
