package com.dwb.caves.control.pathfinding;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class PathFinder {
	/* Manages pathfinding as a wide concept : it includes range finding, 
	 * as the methods to find a path provide us information about the distance between the starting point and any
	 * other node in the grid.
	 * 
	 * The basic principle is to have a loop acting as a wave of tests.
	 * We test the neighbors of our starting node. If they are accessible, they are added to the buffer list.
	 * Then, for each node in the buffer list, we test its neighbors and put it in the closed list, removing it 
	 * from the buffer list. This "wave" enables us to get, in the same method, all nodes within range and the shortest
	 * path to each one.
	 */
	
	private static PathFinder instance;
	public static PathFinder getInstance() {
		if(instance==null)
			instance = new PathFinder();
		return instance;
	}
	// List storing all nodes;
	public Set<PathNode> nodeList;

	// List storing nodes whose neighbors have been tested. They mustn't be tested again or we'll get an infinite loop.
	public Set<PathNode> closedList; 

	// List storing nodes that have been identified as accessible in the previous step of the list.
	public Set<PathNode> bufferList;
	public Set<PathNode> testList;

	private PathNode[][] nodes; 

	public static void setInstance(PathFinder instance) {
		PathFinder.instance = instance;
	}

	public List<PathNode> findPath(PathNode endNode){
		/* Traces the path from the root node (the one used as start node for range calculation) to endNode. 
		 * MaxHeight gives the maximum height difference between two consecutive nodes,
		 * maxDistance gives the maximum distance an element can go during a round.
		 */
		List<PathNode> path = new ArrayList<PathNode>();
		PathNode temp = endNode;
		while(temp!=null){
			path.add(temp);
			temp=temp.getMotherNode();
		}
		return path;
	}

	public Set<PathNode> nodesInRange(PathNode startNode, int maxRange, int maxHeight){
		/* Each iteration of the loop expands the test area until no other node is within range.
		 * When the loop is over, we return the closed list.
		 */
		initialiseNodes();
		boolean iterationNeeded=true;
		testLinkedNodes(startNode, maxRange, maxHeight);
		while (iterationNeeded) {
			iterationNeeded = false;
			
			testList=new HashSet<PathNode>(bufferList);
			bufferList=new HashSet<PathNode>();
			for (PathNode node : testList) {
				iterationNeeded = true;
				testLinkedNodes(node, maxRange, maxHeight);
			}
			
		}
		return closedList;
	}

	private void testLinkedNodes (PathNode node, int maxRange, int maxHeight){
		for(PathNode neighbor : node.getLinkedNodes()){
			if (Math.abs(node.getHeight()-neighbor.getHeight()) < maxHeight) {
				int FWeight = node.getFWeight() + neighbor.getMovementCost();
				if (FWeight <= maxRange) {
					if (!(closedList.contains(neighbor) || testList
							.contains(neighbor))) {
						if (bufferList.contains(neighbor)) {
							if (FWeight < neighbor.getFWeight()
									&& Math.abs(node.getHeight()
											- neighbor.getHeight()) <= maxHeight) {
								neighbor.setFWeight(FWeight);
								neighbor.setMotherNode(node);
							}
						} else {
							neighbor.setFWeight(FWeight);
							bufferList.add(neighbor);
							neighbor.setMotherNode(node);
						}

					}
				}
			}
		}
		closedList.add(node);
	}

	private PathFinder(){
		setNodes(new  PathNode[10][10]);
		nodeList = new HashSet<PathNode>();
		
		for(int i=0; i<10; i++){
			for(int j=0; j<10; j++){
				getNodes()[i][j]=new PathNode(i, j);
				nodeList.add(getNodes()[i][j]);
			}
		}
		
		for(int i=0; i<10; i++){
			for(int j=0; j<10; j++){
				Set<PathNode> set = new HashSet<PathNode>();
				if(i>0)
					set.add(getNodes()[i-1][j]);
				if(i<9)
					set.add(getNodes()[i+1][j]);
				if(j>0)
					set.add(getNodes()[i][j-1]);
				if(j<9)
					set.add(getNodes()[i][j+1]);
				set.remove(getNodes()[i][j]);
				getNodes()[i][j].setLinkedNodes(set);
			}
		}
	}

	private void initialiseNodes() {
		bufferList= new HashSet<PathNode>();
		testList= new HashSet<PathNode>();
		closedList= new HashSet<PathNode>();
		for(PathNode node : nodeList){
			node.setFWeight(0);
			node.setMotherNode(null);
		}
	}

	public PathNode[][] getNodes() {
		return nodes;
	}

	public void setNodes(PathNode[][] nodes) {
		this.nodes = nodes;
	}
}