package com.dwb.caves.control.skills.announcements;

import java.util.Set;

import com.dwb.caves.control.character.GameCharacter;
import com.dwb.caves.control.skills.Skill;

public class Announcement_SkillUsed extends Announcement {
	
	public void dispatch(AnnouncementListener listener){
		listener.handle(this);
	}
	
	public Skill skillUsed;
	public Set<GameCharacter> targets;
	public GameCharacter source;
	
	public Announcement_SkillUsed(Skill skillUsed, GameCharacter source, Set<GameCharacter> targets) {
		super();
		this.skillUsed = skillUsed;
		this.targets = targets;
		this.source = source;
	}
}