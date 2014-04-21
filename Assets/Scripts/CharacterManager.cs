using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CharacterManager {
	
	private static List<GameObject> characters = new List<GameObject>();
	
	private static int count = -1;
	public static void AddCharacter(GameObject character) {
		characters.Add(character);
	}
	
	public static GameObject GetActiveCharacter() {
		if(count == characters.Count - 1) {
			count = -1;
		} 
		count ++;
		return characters[count];
	}
		
}


