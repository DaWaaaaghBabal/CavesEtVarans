package com.dwb.caves.control.skills.factory;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.jdom.Element;
import org.jdom.JDOMException;
import org.jdom.input.SAXBuilder;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.character.statistics.Statistic;
import com.dwb.caves.control.skills.Skill;
import com.dwb.caves.control.skills.SkillCost;
import com.dwb.caves.control.skills.SkillEffect;
import com.dwb.caves.control.skills.SkillHitCondition;
import com.dwb.caves.control.skills.conditions.Condition_Attack;
import com.dwb.caves.control.skills.effects.Effect_CriticalDamage;
import com.dwb.caves.control.skills.effects.Effect_Damage;
import com.dwb.caves.control.skills.values.StatReader;
import com.dwb.caves.control.skills.values.ValueCalculator;
import combat.RepeatedCombatTest_Selector;

public class SkillFactory {

	public static Skill initAttack(GameCharacter attacker,
			GameCharacter defender) {
		Skill attack = new Skill("Basic Attack");
		attack.setCost(new SkillCost());
		attack.setOwner(attacker);

		RepeatedCombatTest_Selector selector = new RepeatedCombatTest_Selector(
				attack);
		selector.target = defender;
		attack.setTargetSelector(selector);

		SkillHitCondition condition = new Condition_Attack(Statistic.MELEE_ATK,
				Statistic.DEFENCE);
		attack.setHitCondition(condition);

		List<SkillEffect> effects = new ArrayList<SkillEffect>();
		attack.setMissEffects(effects);

		effects = new ArrayList<SkillEffect>();
		ValueCalculator dmgValue = new StatReader("source", Statistic.MELEE_DMG);
		SkillEffect damage = new Effect_Damage(dmgValue, "Physical");
		effects.add(damage);
		attack.setHitEffects(effects);

		effects = new ArrayList<SkillEffect>();
		SkillEffect critical = new Effect_CriticalDamage(dmgValue, "Physical");
		effects.add(critical);
		attack.setCritEffects(effects);
		return attack;
	}

	public static Map<String, Skill> initSkills(String className) {
		// TODO Auto-generated method stub
		URL resource = SkillFactory.class.getResource("/XML/skills/" + className + ".xml");
		System.out.println(resource);
		SAXBuilder sxb = new SAXBuilder();
		try {
			Element root = sxb.build(resource).getRootElement();

		} catch (JDOMException | IOException e) {
			e.printStackTrace();
		}
		return null;
	}

}
