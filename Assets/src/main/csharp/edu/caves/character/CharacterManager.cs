using System;
using System.Collections.Generic;
using System.Linq;

namespace CavesEtVarans
{
	public class CharacterManager
	{
		private static CharacterManager instance;
		
		public static CharacterManager get() {
			if(instance == null) {
				instance = new CharacterManager();
			}
			return instance;
		}
		
		private CharacterManager ()
		{
			
		}
		
		private LinkedList<Character> characters = null;
		
		protected LinkedList<Character> GetCharacters() {
			if(characters == null) {
				characters = new LinkedList<Character>();
			}
			return characters;
		}
		
		public void add(Character character) {
			GetCharacters().AddLast(character);
		}
		
		public void remove(Character character) {
			GetCharacters().Remove(character);
		}
		
		// TODO instanciation initiale
		private LinkedListNode<Character> activeCharacterNode = null;
		
		public void activateNext() {
			activeCharacterNode.Value.Activate();
			activeCharacterNode = activeCharacterNode.Next;
            // TODO vérification de bouclage
		}

        public Character getActiveCharacter()
        {
            return activeCharacterNode.Value;
        }

	}
}

