package com.dwb.caves.control.character.statistics.modifiers;

import com.dwb.caves.control.character.statistics.StatisticsManager;

public abstract class StatMultiplier extends StatModifier {

	public StatMultiplier(String stat) {
		super(stat);
	}

	public void dispatchAdd(StatisticsManager statisticsManager) {
		statisticsManager.getStat(modifiedStat).addMultiplier(this);
	}

	public void dispatchRemove(StatisticsManager statisticsManager) {
		statisticsManager.getStat(modifiedStat).removeMultiplier(this);
	}
}
