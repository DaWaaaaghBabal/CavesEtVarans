package com.dwb.caves.control.skills.effects;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.skills.SkillContext;
import com.dwb.caves.control.skills.SkillEffect;
import com.dwb.caves.control.skills.values.ValueCalculator;

public class Effect_Damage extends SkillEffect {

	protected ValueCalculator value;
	private String damageType;
		
	public Effect_Damage(ValueCalculator value, String type) {
		this.value = value;
		damageType = type;
	}
	
	public void apply(SkillContext context) {
		GameCharacter target = (GameCharacter) context.get("current target");
		target.takeDamage((int) value.value(context), damageType);
	}


}
