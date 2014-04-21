package com.dwb.caves.control.skills.announcements;

public abstract class Announcement {

	public void dispatch(AnnouncementListener listener){
		listener.handle(this);
	}
}
