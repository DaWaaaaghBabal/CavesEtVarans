package com.dwb.caves.control.pathfinding;

import java.util.HashSet;
import java.util.Set;

public class PathNode {

	private int i;
	public int getI() {
		return i;
	}
	public void setI(int i) {
		this.i = i;
	}
	
	private int j;
	public int getJ() {
		return j;
	}
	public void setJ(int j) {
		this.j = j;
	}
	
	private boolean obstacle;
	public boolean isObstacle(){
		return obstacle;
	}
	public void setObstacle(boolean obstacle) {
		this.obstacle = obstacle;
	}

	private PathNode motherNode;
	public PathNode getMotherNode() {
		return motherNode;
	}
	public void setMotherNode(PathNode motherNode) {
		this.motherNode = motherNode;
	}
	
	private int height;
	public int getHeight() {
		return height;
	}
	public void setHeight(int height) {
		this.height = height;
	}

	private int FWeight;
	public int getFWeight() {
		return FWeight;
	}
	public void setFWeight(int fWeight) {
		FWeight = fWeight;
	}

	private Set<PathNode> linkedNodes;
	public Set<PathNode> getLinkedNodes() {
		return linkedNodes;
	}
	public void setLinkedNodes(Set<PathNode> linkedNodes) {
		this.linkedNodes = linkedNodes;
	}
	
	public int getMovementCost() {
		return 1;
	}
	
	public PathNode(int i, int j){
		setObstacle(false);
		setI(i);
		setJ(j);
		setFWeight(0);
		setHeight(0);
		setLinkedNodes(new HashSet<PathNode>());
	}
	
	public String toString(){
		return "Node "+getI()+";"+getJ();
	}
	
}
