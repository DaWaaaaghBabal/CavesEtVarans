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

        public List<Character> Characters { private set; get; }
		private Character activeCharacter;
        private Context context;

        private CharacterManager ()
		{
            Characters = new List<Character>();
            context = Context.ProvideTurnOrderContext();
        }

		public Character ActiveCharacter {
            get {
                if (activeCharacter == null) {
                    SearchForNewActiveCharacter();
                }
                return activeCharacter;
            }
            private set { activeCharacter = value; }
        }

        public void Add (Character character) {
            Characters.Add(character);
		}
		
		public void Remove (Character character) {
			Characters.Remove(character);
		}

		public void ActivateNext () {
			SearchForNewActiveCharacter();
			ActiveCharacter.Activate();
		}

		protected void SearchForNewActiveCharacter () {
            // Search for all characters that areready to act
            List<Character> ready = new List<Character>();
            foreach (Character c in Characters) {
                if (c.AP >= c.GetStatValue(Statistic.ACTION_AP, context)) {
                    ready.Add(c);
                }
            }
            if (ready.Count > 0) {
                // Some characters are ready to act : sort them by AP, then by Initiative, and return the first.
                ready.Sort(Compare);
                ActiveCharacter = ready[0];
            } else {
                Random rand = new Random();
                // No character was ready to act : fill all AP gauges, then start again.
                foreach (Character c in Characters) {
                    // The amount of AP gained by each character is slightly randomised, to mess with the turn order.
                    c.IncrementResource(resource.Resource.AP, c.GetStatValue(Statistic.INITIATIVE, context) + rand.Next(1, 4));
                }
                SearchForNewActiveCharacter();
            }
		}

		public void Clear() {
			Characters.Clear();
			ActiveCharacter = null;
		}

        private int Compare(Character c1, Character c2) {
            if (c1.AP > c2.AP) return 1;
            if (c2.AP > c1.AP) return -1;
            // Same AP, we break the tie by comparing Initiative
            int init1 = c1.GetStatValue(Statistic.INITIATIVE, context);
            int init2 = c2.GetStatValue(Statistic.INITIATIVE, context);
            return init1 - init2;
        }
	}

}

