package com.dwb.caves.control.skills.announcements;

public interface AnnouncementListener{

	public void handle(Announcement announcement);
	
	public void dispatch(Announcement announcement);
	
}
