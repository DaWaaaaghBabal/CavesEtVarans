package com.dwb.caves.control.skills.announcements;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.skills.Skill;

public class Announcement_SkillMiss extends Announcement {

	public void dispatch(AnnouncementListener listener){
		listener.handle(this);
	}
	
	public Skill skillUsed;
	public GameCharacter target, source;
	public Announcement_SkillMiss(Skill skillUsed, GameCharacter source,
			GameCharacter target) {
		super();
		this.skillUsed = skillUsed;
		this.target = target;
		this.source = source;
	}
	
}