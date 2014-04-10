package com.dwb.caves.control.skills;


public abstract class SkillTargetSelector {

	protected Skill skill;
	
	public SkillTargetSelector(Skill skill) {
		super();
		this.skill = skill;
	}

	public abstract void pickTargets(SkillContext context);

}
