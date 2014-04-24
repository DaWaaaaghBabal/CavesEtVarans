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
		
		private LinkedList<Character> characters = null;
		
		protected LinkedList<Character> getCharacters() 
        {
			if(characters == null) 
            {
				characters = new LinkedList<Character>();
			}
			return characters;
		}
		
		public void Add(Character character) 
        {
			getCharacters().AddLast(character);
		}
		
		public void remove(Character character) 
        {
			getCharacters().Remove(character);
		}

        public void activateNext()
        {
            // loop on every characters until one of them hits the upper bar of action
            Boolean up;
            do {
                up = false;
                foreach(Character c in getCharacters()) 
                {
                    up |= c.runAction();
                }
            } while(!up);
            // pick the first character with the upper amount of A.P.
            getActiveCharacter().Activate();
        }

        public Character getActiveCharacter()
        {
            Character active = getCharacters().First();
            foreach (Character c in getCharacters())
            {
                if (c.GetAP() > active.GetAP())
                {
                    active = c;
                }
            }
            return active;
        }

        internal void Clear()
        {
            getCharacters().Clear();
        }
    }

}

