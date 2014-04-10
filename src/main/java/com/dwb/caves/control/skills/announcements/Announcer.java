package com.dwb.caves.control.skills.announcements;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;


public class Announcer {
	
	private Map<Class<Announcement>, Set<AnnouncementListener>> registry;
	
	public Announcer(){
		registry = new HashMap<Class<Announcement>, Set<AnnouncementListener>>();
	}
	
	public void addListener(AnnouncementListener listener, Class<Announcement> announcementClass){
		if(registry.get(announcementClass) == null){
			registry.put(announcementClass, new HashSet<AnnouncementListener>());
		}
		registry.get(announcementClass).add(listener);
	}
	
	public void removeListener(AnnouncementListener listener, Class<Announcement> announcementClass){
		if(registry.get(announcementClass) != null)
			registry.get(announcementClass).remove(listener);
	}
	
	public void handleAnnouncement(Announcement announcement){
		if(registry.keySet().contains(announcement.getClass())){
			for(AnnouncementListener listener : registry.get(announcement.getClass())){
				listener.dispatch(announcement);
			}
		}
	}

	private static Announcer instance;
	public static Announcer getInstance() {
		if(instance == null) instance = new Announcer();
		return instance;
	}

}

