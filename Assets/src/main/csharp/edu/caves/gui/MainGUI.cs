using System;
using System.Collections.Generic;
using UnityEngine;
using CavesEtVarans;

namespace CavesEtVarans {
    class MainGUI : MonoBehaviour {

        private static Character activeCharacter;
        
        public static void DisplayCharacter(Character newActive) {
            activeCharacter = newActive;
        }

        void Start() {
            CharacterManager.Get().Add(new Character("Character A"));
            CharacterManager.Get().Add(new Character("Character B"));
            CharacterManager.Get().ActivateNext();
        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 30), "End turn")) {
                activeCharacter.EndTurn();
                Debug.Log("Activating next character", null);
            }
            if (activeCharacter != null) {
                GUI.Label(new Rect(10, 45, 100, 20), activeCharacter.GetName());
                int y = 75;
                DisplayStat(Statistic.HEALTH, "Max health", 10, y);
                y += 20;
                DisplayStat(Statistic.DEFENSE, "Defense", 10, y);
                y += 20;
                DisplayStat(Statistic.DODGE, "Dodge", 10, y);
                y += 20;
                DisplayStat(Statistic.MELEE_ATTACK, "Melee attack", 10, y);
                y += 20;
                DisplayStat(Statistic.MELEE_DAMAGE, "Melee damage", 10, y);
                y += 30;
                GUI.Label(new Rect(10, y, 100, 20), "AP");
                GUI.Label(new Rect(115, y, 80, 20), activeCharacter.GetAP().ToString());
            }
        }

        private void DisplayStat(string key, string label, int X, int Y) {
            GUI.Label(new Rect(X, Y, 100, 20), label);
            GUI.Label(new Rect(X + 105, Y, 80, 20), activeCharacter.GetStatValue(key).ToString());
        }
    }
}
