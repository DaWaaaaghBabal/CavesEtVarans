package com.dwb.caves.control.skills.conditions;

import com.dwb.caves.control.character.statistics.Statistic;
import com.dwb.caves.control.generalTools.game.Dice;
import com.dwb.caves.control.skills.Skill;
import com.dwb.caves.control.skills.SkillContext;
import com.dwb.caves.control.skills.SkillHitCondition;
import com.dwb.caves.control.skills.values.StatReader;
import com.dwb.caves.control.skills.values.ValueCalculator;

public class Condition_Attack extends SkillHitCondition {

	private ValueCalculator 
	attackStat, 
	defenceStat, 
	sourceHitModifier, 
	targetHitModifier, 
	criticalProbability;
	
	public Condition_Attack(String attackStr, String defenceStr) {
		attackStat = new StatReader("source", attackStr);
		defenceStat = new StatReader("current target", defenceStr);
		sourceHitModifier = new StatReader("source", Statistic.HIT_MOD_ATK);
		targetHitModifier = new StatReader("current target", Statistic.HIT_MOD_DEF);
		criticalProbability = new StatReader("current target", Statistic.CRIT_CHANCE);
	}



	public void test(SkillContext context) {
		/* This is actually simpler than it looks : the calculation of the hit probability is set by the game rules.
		 * We read the various stats needed in the calculation and get our hit and crit probabilities.
		 */		
		int ATK = (int) attackStat.value(context);
		int DEF = (int) defenceStat.value(context);
		int baseProbability = 50 + (int)sourceHitModifier.value(context) + (int)targetHitModifier.value(context);
		int hitProbability = baseProbability + (70*(ATK-DEF)/(ATK+DEF));
		int critProbability = (hitProbability*(int)(criticalProbability.value(context))/100);
		
		// Now that we know the probability, we roll the dice. If die roll < probability, it's a success.
		Skill skill = (Skill) context.get("skill");
		if(Dice.roll()<=hitProbability)
			if(Dice.roll()<=critProbability)
				skill.critical(context);
			else
				skill.hit(context);
		else
			skill.miss(context);
	}

}
