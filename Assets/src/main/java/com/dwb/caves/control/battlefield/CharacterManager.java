package com.dwb.caves.control.battlefield;

import java.util.HashSet;
import java.util.Observable;
import java.util.Set;

import com.dwb.caves.control.character.GameCharacter;

public class CharacterManager extends Observable{
	/* This class basically gives a list of all characters on the field.
	 * Later improvements will include sorting the list by initiative order, keeping trace of current active player, etc.
	 */
	private Set<GameCharacter> characters;
	
	private static CharacterManager instance;
	public static CharacterManager getInstance(){
		if (instance==null){
			instance=new CharacterManager();
		}
		return instance;
	}
	
	private CharacterManager(){
		characters=new HashSet<GameCharacter>();
	}
	
	public void add(GameCharacter character){
            characters.add(character);
	}
	public Set<GameCharacter> getCharacterList(){
		return characters;
	}
}
