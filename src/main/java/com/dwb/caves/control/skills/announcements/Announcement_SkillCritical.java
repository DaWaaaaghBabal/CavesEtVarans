package com.dwb.caves.control.skills.announcements;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.skills.Skill;

public class Announcement_SkillCritical extends Announcement {

	public Skill skill;
	public GameCharacter target, source;
	public Announcement_SkillCritical(Skill skill, GameCharacter source,
			GameCharacter target) {
		super();
		this.skill = skill;
		this.target = target;
		this.source = source;
	}
}
