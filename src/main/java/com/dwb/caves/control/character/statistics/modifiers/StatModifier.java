package com.dwb.caves.control.character.statistics.modifiers;

import com.dwb.caves.control.character.statistics.StatisticsManager;
import com.dwb.caves.control.skills.SkillContext;

public abstract class StatModifier {

	public abstract float getModifiedValue(float value, SkillContext context);
	public abstract void dispatchAdd(StatisticsManager statisticsManager);
	public abstract void dispatchRemove(StatisticsManager statisticsManager);

	protected String modifiedStat;
	
	public StatModifier(String stat){
		modifiedStat = stat;
	}

}
