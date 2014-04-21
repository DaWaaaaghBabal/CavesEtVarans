package com.dwb.caves.control.character.statistics;

public class ProgressiveStatistic extends Statistic{

	public ProgressiveStatistic(float value, int upgradeCost) {
		super(value, upgradeCost);
	}

	public void levelUp() {
		value *= 1.04;
	}

}
