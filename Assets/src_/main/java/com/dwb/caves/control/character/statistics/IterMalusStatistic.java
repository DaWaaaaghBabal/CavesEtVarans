package com.dwb.caves.control.character.statistics;

public class IterMalusStatistic extends Statistic {

	private float perLevel;
	public IterMalusStatistic(float value, int upgradeCost, float perLevel) {
		super(value, upgradeCost);
		this.perLevel = perLevel;
	}

	public void levelUp() {
		value -= perLevel;
	}
	
	public void upgrade(){
		value --;
	}

}
