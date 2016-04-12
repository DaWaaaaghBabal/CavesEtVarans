using System;
using CavesEtVarans.character;
using CavesEtVarans.character.resource;
using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using UnityEngine;
using System.Collections.Generic;

namespace CavesEtVarans.gui
{
	class MainGUI : MonoBehaviour
	{

		public int iconSize;
		public CharacterDisplay activeCharacterDisplay;

		private static Character activeCharacter;
		private static Character displayedCharacter;
		private static Context context = Context.ProvideDisplayContext();
		public static void ActivateCharacter (Character newActive)
		{
			activeCharacter = newActive;
		}

		public static void DisplayCharacter (Character newDisplay)
		{
			displayedCharacter = newDisplay;
		}

		void Start ()
		{
			instance = this;
		}
        void Update() {
            if (Input.GetMouseButton(1)) GUIEventHandler.Get().HandleRightClick();
        }
		void OnGUI ()
		{
			if (GUI.Button(new Rect(10, 10, 100, 30), "End turn")) {
				activeCharacter.EndTurn();
                GUIEventHandler.Get().Reset();
				CharacterManager.Get().ActivateNext();
			}
			if (activeCharacter != null) 
				DisplayCharacterStats (activeCharacter, 75);
			if (displayedCharacter != null) 
				DisplayCharacterStats (displayedCharacter, 375);
			DisplaySkills();
		}

		private void DisplaySkills() {
			List<Skill> skills = activeCharacter.Skills;
			int height = Screen.height;
			int width = Screen.width;
			int number = skills.Count;
			int X = (width - number * iconSize) / 2;
			int Y = height - iconSize - 10;

			foreach (Skill skill in skills) {
				DisplaySkill(skill, X, Y);
				X += iconSize;
			}
		}

		private void DisplaySkill(Skill skill, int X, int Y) {
			string iconName = skill.Attributes.Icon;
			Texture2D icon = Resources.Load<Texture2D>(iconName);
			if (GUI.Button(new Rect(X, Y, iconSize, iconSize), icon)) {
				skill.InitSkill(activeCharacter);
			}
		}

		private void DisplayCharacterStats (Character character, int Y)
		{
			int y = Y;

			GUI.Label (new Rect (10, y, 100, 20), character.Name);
			y += 30;
			//DisplayStat (character, Statistic.HEALTH, "Max health", 10, y);
			DisplayHealth(character, 10, y);
			y += 20;
			DisplayStat (character, Statistic.DEFENSE, "Defense", 10, y);
			y += 20;
			DisplayStat (character, Statistic.DODGE, "Dodge", 10, y);
			y += 20;
			DisplayStat (character, Statistic.ATTACK, "Attack", 10, y);
			y += 30;
			GUI.Label (new Rect (10, y, 100, 20), "AP");
			GUI.Label (new Rect (115, y, 80, 20), character.GetResourceAmount(Resource.AP).ToString ());
		}

		private void DisplayStat(Character character, string key, string label, int X, int Y) {
			GUI.Label(new Rect(X, Y, 100, 20), label);
			GUI.Label(new Rect(X + 105, Y, 80, 20), character.GetStatValue(key, context).ToString());
		}
		private void DisplayHealth(Character character, int X, int Y) {
			GUI.Label(new Rect(X, Y, 100, 20), "Health");

			string healthText = character.GetResourceAmount(Resource.HP).ToString();
			healthText += " / " + character.GetStatValue(Statistic.HEALTH, context);

			GUI.Label(new Rect(X + 105, Y, 80, 20), healthText);
		}

		private static MainGUI instance;
		public static MainGUI GetInstance ()
		{
			return instance;
		}
	}
}
