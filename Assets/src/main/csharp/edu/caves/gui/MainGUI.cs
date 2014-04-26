using System;
using System.Collections.Generic;
using UnityEngine;
using CavesEtVarans;

namespace CavesEtVarans {
    class MainGUI : MonoBehaviour {

        private static Character activeCharacter;
        private static Character displayedCharacter;

        public static void ActivateCharacter(Character newActive) {
            activeCharacter = newActive;
        }

        public static void DisplayCharacter(Character newDisplay) {
            displayedCharacter = newDisplay;
        }

        void Start() {

        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 30), "End turn")) {
                activeCharacter.EndTurn();
                CharacterManager.Get().ActivateNext();
            }
            if (activeCharacter != null) {
                DisplayCharacterStats(activeCharacter, 75);
            }
            if (displayedCharacter != null) {
                DisplayCharacterStats(displayedCharacter, 375);
            }
        }

        private void DisplayCharacterStats(Character character, int Y) {

            GUI.Label(new Rect(10, Y-30, 100, 20), character.GetName());
            int y = Y;
            DisplayStat(character, Statistic.HEALTH, "Max health", 10, y);
            y += 20;
            DisplayStat(character, Statistic.DEFENSE, "Defense", 10, y);
            y += 20;
            DisplayStat(character, Statistic.DODGE, "Dodge", 10, y);
            y += 20;
            DisplayStat(character, Statistic.MELEE_ATTACK, "Melee attack", 10, y);
            y += 20;
            DisplayStat(character, Statistic.MELEE_DAMAGE, "Melee damage", 10, y);
            y += 30;
            GUI.Label(new Rect(10, y, 100, 20), "AP");
            GUI.Label(new Rect(115, y, 80, 20), activeCharacter.GetAP().ToString());
        }

        private void DisplayStat(Character character, string key, string label, int X, int Y) {
            GUI.Label(new Rect(X, Y, 100, 20), label);
            GUI.Label(new Rect(X + 105, Y, 80, 20), character.GetStatValue(key).ToString());
        }
    }
}
