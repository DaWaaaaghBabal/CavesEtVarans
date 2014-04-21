package com.dwb.caves.control.skills.effects;

import com.dwb.caves.control.character.statistics.Statistic;
import com.dwb.caves.control.skills.values.MultiplierValueCalculator;
import com.dwb.caves.control.skills.values.StatReader;
import com.dwb.caves.control.skills.values.ValueCalculator;

public class Effect_CriticalDamage extends Effect_Damage{

	public Effect_CriticalDamage(ValueCalculator value, String type) {
		super(value, type);
		ValueCalculator criticalMultiplier = new StatReader("source", Statistic.CRIT_MULTIPLIER);
		ValueCalculator compositeValue = new MultiplierValueCalculator(value, criticalMultiplier);
		this.value=compositeValue;
	}

}
