package com.dwb.caves.control.character.statistics.modifiers;

import com.dwb.caves.control.character.statistics.StatisticsManager;

public abstract class StatOverrider extends StatModifier {

	public StatOverrider(String stat) {
		super(stat);
	}

	public abstract float getModifiedValue(float value);

	public void dispatchAdd(StatisticsManager statisticsManager) {
		statisticsManager.getStat(modifiedStat).addOverrider(this);
	}

	public void dispatchRemove(StatisticsManager statisticsManager) {
		statisticsManager.getStat(modifiedStat).removeOverrider(this);
	}

}