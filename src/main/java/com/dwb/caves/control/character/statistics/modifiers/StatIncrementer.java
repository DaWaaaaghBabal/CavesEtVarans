package com.dwb.caves.control.character.statistics.modifiers;

import com.dwb.caves.control.character.statistics.StatisticsManager;

public abstract class StatIncrementer extends StatModifier{

	public StatIncrementer(String stat) {
		super(stat);
	}

	public abstract float getModifiedValue(float value);

	public void dispatchAdd(StatisticsManager statisticsManager) {
		statisticsManager.getStat(modifiedStat).addIncrementer(this);
	}

	public void dispatchRemove(StatisticsManager statisticsManager) {
		statisticsManager.getStat(modifiedStat).removeIncrementer(this);
	}
}
