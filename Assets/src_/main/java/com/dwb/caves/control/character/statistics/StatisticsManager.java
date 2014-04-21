package com.dwb.caves.control.character.statistics;

import java.util.HashMap;
import java.util.Map;
import java.util.Observable;

import com.dwb.caves.control.character.statistics.modifiers.StatModifier;

public class StatisticsManager extends Observable{

	protected static final int MAX_HEALTH_COST = 1;
	protected static final int DEFENCE_COST = 5;
	protected static final int DODGE_COST = 7;
	protected static final int MELEE_ATK_COST = 5;
	protected static final int MELEE_DMG_COST = 3;
	protected static final int RANGE_ATK_COST = 5;
	protected static final int RANGE_DMG_COST = 4;
	protected static final int WILLPOWER_COST = 4;
	protected static final int MAGICAL_POWER_COST = 3;
	protected static final int INITIATIVE_COST = 25;
	protected static final int ITER_MALUS_COST = 8;
	protected static final int JUMP_HEIGHT_COST = 15;
	
	protected Map<String, Statistic> statistics;
	
	public StatisticsManager(){
		statistics = new HashMap<String, Statistic>();
		
		statistics.put(Statistic.MAX_HEALTH, new ProgressiveStatistic(24, MAX_HEALTH_COST));
		statistics.put(Statistic.DEFENCE, new ProgressiveStatistic(8, DEFENCE_COST));
		statistics.put(Statistic.MELEE_ATK, new ProgressiveStatistic(8, MELEE_ATK_COST));
		statistics.put(Statistic.MELEE_DMG, new ProgressiveStatistic(8, MELEE_DMG_COST));
		statistics.put(Statistic.RANGE_ATK, new ProgressiveStatistic(8, RANGE_ATK_COST));
		statistics.put(Statistic.RANGE_DMG, new ProgressiveStatistic(6, RANGE_DMG_COST));
		statistics.put(Statistic.WILLPOWER, new ProgressiveStatistic(8, WILLPOWER_COST));
		statistics.put(Statistic.SPELL_POWER, new ProgressiveStatistic(8, MAGICAL_POWER_COST));
		statistics.put(Statistic.DODGE, new ProgressiveStatistic(6, DODGE_COST));
		
		statistics.put(Statistic.INITIATIVE, new FixedStatistic(8, INITIATIVE_COST));
		statistics.put(Statistic.JUMP_HEIGHT, new FixedStatistic(2, JUMP_HEIGHT_COST));
		
		statistics.put(Statistic.ITER_MALUS, new IterMalusStatistic(25, ITER_MALUS_COST, 0.1F));
		
		statistics.put(Statistic.HIT_MOD_DEF, new FixedStatistic(0, 0));
		statistics.put(Statistic.HIT_MOD_ATK, new FixedStatistic(0, 0));
		statistics.put(Statistic.CRIT_MULTIPLIER, new FixedStatistic(1.5F, 0));
		statistics.put(Statistic.CRIT_CHANCE, new FixedStatistic(40, 0));
		
		statistics.put(Statistic.CLASS_STAT, new ProgressiveStatistic(6, 7));
		statistics.put(Statistic.ENERGY_POOL, new FixedStatistic(3, 5));
	}

	public Statistic getStat(String str){
		return statistics.get(str);
	}
	
	public void addStatModifier(StatModifier modifier){
		modifier.dispatchAdd(this);
	}
	public void removeStatModifier(StatModifier modifier){
		modifier.dispatchRemove(this);
	}
}