using CavesEtVarans.character.statistics;
using CavesEtVarans.skills.core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CavesEtVarans.character
{
	public class CharacterManager
	{
		private static CharacterManager instance;
		
		public static CharacterManager Get ()
		{
			if (instance == null) {
				instance = new CharacterManager ();
			}
			return instance;
		}
		
		private CharacterManager ()
		{
			
		}

		private Character activeCharacter = null;

		public Character GetActiveCharacter ()
		{
			if (activeCharacter == null) {
				SearchForNewActiveCharacter ();
			}
			return activeCharacter;
		}

		private LinkedList<Character> characters = null;
		
		protected LinkedList<Character> GetCharacters ()
		{
			if (characters == null) {
				characters = new LinkedList<Character> ();
			}
			return characters;
		}
		
		public void Add (Character character)
		{
			GetCharacters ().AddLast (character);
		}
		
		public void Remove (Character character)
		{
			GetCharacters ().Remove (character);
		}

		public void ActivateNext ()
		{
			SearchForNewActiveCharacter ();
			GetActiveCharacter ().Activate ();
		}

		protected void SearchForNewActiveCharacter ()
		{
			//@TODO initialize a turn loop context here
			Context context = Context.Init(null, null);
			// loop on every characters until one of them hits the upper bar of action
			Boolean up;
			do {
				up = false;
				foreach (Character c in GetCharacters()) {   
					// The amount of AP gained by each character is slightly randomised, to mess with the turn order.
					c.IncrementAP(c.GetStatValue(Statistic.INITIATIVE, context) + new Random().Next(1, 4));
					up |= (c.AP >= c.GetStatValue(Statistic.ACTION_AP, context));
				}
			} while (!up);

			// pick the first character with the higher amount of A.P. and higher Initiative value if both A.P. are eq.
			Character active = GetCharacters ().First ();
			foreach (Character c in GetCharacters()) {
				if (c.AP > active.AP
					|| (c.AP == active.AP && c.GetStatValue(Statistic.INITIATIVE, context) > active.GetStatValue(Statistic.INITIATIVE, context))
                ) {
					active = c;
				}
			}
			activeCharacter = active;
		}

		internal void Clear ()
		{
			GetCharacters ().Clear ();
			activeCharacter = null;
		}

	}

}

