using System;
using System.Collections.Generic;
using System.Linq;

namespace CavesEtVarans
{
	public class CharacterManager
	{
		private static CharacterManager instance;
		
		public static CharacterManager Get() 
        {
			if(instance == null) 
            {
				instance = new CharacterManager();
			}
			return instance;
		}
		
		private CharacterManager ()
		{
			
		}

        private Character activeCharacter = null;

        public Character GetActiveCharacter()
        {
            if (activeCharacter == null)
            {
                SearchForNewActiveCharacter();
            }
            return activeCharacter;
        }

		private LinkedList<Character> characters = null;
		
		protected LinkedList<Character> GetCharacters() 
        {
			if(characters == null) 
            {
				characters = new LinkedList<Character>();
			}
			return characters;
		}
		
		public void Add(Character character) 
        {
			GetCharacters().AddLast(character);
		}
		
		public void Remove(Character character) 
        {
			GetCharacters().Remove(character);
		}

        public void ActivateNext()
        {
            GetActiveCharacter().EndTurn();
            SearchForNewActiveCharacter();
            GetActiveCharacter().Activate();

        }

        protected void SearchForNewActiveCharacter()
        {
            // loop on every characters until one of them hits the upper bar of action
            Boolean up;
            do
            {
                up = false;
                foreach (Character c in GetCharacters())
                {
                    c.IncrementAP(c.GetStatValue(Statistic.INITIATIVE) + Dice.Roll(1, 3));
                    up |= (c.GetAP() >= c.GetStatValue(Statistic.ACTION_AP));
                }
            } while (!up);

            // pick the first character with the higher amount of A.P. and higher Initiative value if both A.P. are eq.
            Character active = GetCharacters().First();
            foreach (Character c in GetCharacters())
            {
                if (c.GetAP() > active.GetAP()
                    || (c.GetAP() == active.GetAP() && c.GetStatValue(Statistic.INITIATIVE) > active.GetStatValue(Statistic.INITIATIVE))
                )
                {
                    active = c;
                }
            }
            activeCharacter = active;
        }

        internal void Clear()
        {
            GetCharacters().Clear();
        }

    }

}

