package com.dwb.caves.control.character.statistics;

public class FixedStatistic extends Statistic {

	public FixedStatistic(float value, int upgradeCost) {
		super(value, upgradeCost);
	}

	public void levelUp() {
		// "fixed" statistics, so nothing happens on level up.
	}

}
