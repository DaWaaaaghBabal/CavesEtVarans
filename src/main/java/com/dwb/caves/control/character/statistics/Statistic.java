package com.dwb.caves.control.character.statistics;

import java.util.ArrayList;
import java.util.List;

import com.dwb.caves.control.character.statistics.modifiers.StatIncrementer;
import com.dwb.caves.control.character.statistics.modifiers.StatMultiplier;
import com.dwb.caves.control.character.statistics.modifiers.StatOverrider;
import com.dwb.caves.control.skills.SkillContext;

public abstract class Statistic {

	public static final String MAX_HEALTH = "MAX HP";
	public static final String ENERGY_POOL = "EP";
	public static final String CLASS_STAT = "Class";
	public static final String ITER_MALUS = "IM";
	public static final String INITIATIVE = "I";
	public static final String JUMP_HEIGHT = "JH";
	public static final String DODGE = "D";
	public static final String SPELL_POWER = "SPP";
	public static final String WILLPOWER = "WILL";
	public static final String RANGE_DMG = "RDMG";
	public static final String RANGE_ATK = "RATK";
	public static final String MELEE_DMG = "MDMG";
	public static final String MELEE_ATK = "MATK";
	public static final String DEFENCE = "DEF";
	public static final String HIT_MOD_DEF = "Hit Modifier, DEF";
	public static final String HIT_MOD_ATK = "Hit Modifier, ATK";
	public static final String CRIT_CHANCE = "CRIT_CHANCE";
	public static final String CRIT_MULTIPLIER = "CRIT_MULT";

	protected float value;
	private List<StatMultiplier> statMultipliers;
	private List<StatIncrementer> statIncrementers;
	private List<StatOverrider> statOverriders;

	public float getValue(SkillContext context) {
		// Multipliers are applied first, then fixed bonuses (incrementers),
		// then the resulting value may be overridden.
		float result = value;
		for (StatMultiplier multiplier : statMultipliers)
			result = multiplier.getModifiedValue(result, context);
		for (StatIncrementer incrementer : statIncrementers)
			result = incrementer.getModifiedValue(result, context);
		for (StatOverrider overrider : statOverriders)
			result = overrider.getModifiedValue(result, context);
		return result;
	}

	private int upgradeCost;

	public int getUpgradeCost() {
		return upgradeCost;
	}

	void upgrade() {
		value++;
	}

	public abstract void levelUp();

	public void addMultiplier(StatMultiplier multiplier) {
		statMultipliers.add(multiplier);
	}

	public void removeMultiplier(StatMultiplier multiplier) {
		statMultipliers.remove(multiplier);
	}

	public void addIncrementer(StatIncrementer incrementer) {
		statIncrementers.add(incrementer);
	}

	public void removeIncrementer(StatIncrementer incrementer) {
		statIncrementers.remove(incrementer);
	}

	public void addOverrider(StatOverrider overrider) {
		statOverriders.add(overrider);
	}

	public void removeOverrider(StatOverrider overrider) {
		statOverriders.remove(overrider);
	}

	public Statistic(float value, int upgradeCost) {
		this.value = value;
		this.upgradeCost = upgradeCost;
		statMultipliers = new ArrayList<StatMultiplier>();
		statIncrementers = new ArrayList<StatIncrementer>();
		statOverriders = new ArrayList<StatOverrider>();
	}
}
