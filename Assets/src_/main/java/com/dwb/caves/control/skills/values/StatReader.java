package com.dwb.caves.control.skills.values;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.skills.SkillContext;

public class StatReader extends ValueCalculator {

	// Whose statistic will I read ? Can be "source" or "current target".
	private String characterKey;
	// Which statistic will I read ?
	private String statisticKey;
	
	public StatReader(String charKey, String statKey) {
		characterKey = charKey;
		statisticKey = statKey;
	}

	public float value(SkillContext context) {
		// As characterKey is either "source" or "current target", we know the corresponding data in the context is always a GameCharacter.
		GameCharacter charac = (GameCharacter) context.get(characterKey);
		return charac.getStatistic(statisticKey, context);
	}

}
