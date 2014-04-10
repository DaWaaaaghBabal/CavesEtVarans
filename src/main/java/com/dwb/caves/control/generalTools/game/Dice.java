package com.dwb.caves.control.generalTools.game;

public class Dice {
	public static int roll(int X, int Y, int Z){
		//returns XdY+Z
		int result=Z;
		for(int i=0; i<X; i++){
			result+=1+Math.floor(Math.random()*Y);
		}
		return result;
	}
	
	public static int roll(int X, int Y){
		return roll(X, Y, 0);
	}

	//Default : 1d100
	public static int roll() {
		return roll(1,100,0);
	}
}
