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
            MainGUI.DisplayCharacter(new Character("Character1"));
        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 30), "End turn")) {
                //CharacterManager.get().activateNext();
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
            }
        }

        private void DisplayStat(string key, string label, int X, int Y) {
            GUI.Label(new Rect(X, Y, 100, 20), label);
            GUI.Label(new Rect(X + 105, Y, 80, 20), activeCharacter.getStatValue(key).ToString());
        }
    }
}
