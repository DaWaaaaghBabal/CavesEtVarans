package com.dwb.caves.control.character;

import java.awt.Point;
import java.util.Observable;

import com.dwb.caves.control.battlefield.CharacterManager;
import com.dwb.caves.control.character.statistics.StatisticsManager;
import com.dwb.caves.control.skills.SkillContext;
import com.dwb.caves.control.skills.SkillCost;

public class GameCharacter extends Observable {

	private StatisticsManager statisticsManager;
	public StatisticsManager getStatisticsManager(){
		return statisticsManager;
	}
	public void setStatisticsManager(StatisticsManager arg){
		statisticsManager=arg;
	}
	
	private int hitPoints;
	public int getHitPoints(){
		return hitPoints;
	}
	public void setHitPoints(int hitPoints) {
		this.hitPoints=hitPoints;
		setChanged();
		notifyObservers();
	}
	
	private int actionPoints;
	public int getActionPoints(){
		return actionPoints;
	}
	public void setActionPoints(int actionPoints) {
		this.actionPoints=actionPoints;
		setChanged();
		notifyObservers();
	}

	private Point gridPosition;
	public Point getGridPosition() {
		return gridPosition;
	}
	public void setGridPosition(Point position) {
		this.gridPosition = position;
	}
	
	private String name;
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	
	public String getController() {
		return null;
	}
	
	public GameCharacter(){
		setStatisticsManager(new StatisticsManager());
		CharacterManager.getInstance().add(this);
		
	}
	
	public static GameCharacter createGameCharacter() {
		return new GameCharacter();
	}

	public void payCost(SkillCost cost) {
		//System.out.println("Paying cost : "+cost);
	}
	public float getStatistic(String statisticKey, SkillContext context) {
		return getStatisticsManager().getStat(statisticKey).getValue(context);
	}
	public void takeDamage(int value, String damageType) {
		setHitPoints(getHitPoints() - value);
	}

}
