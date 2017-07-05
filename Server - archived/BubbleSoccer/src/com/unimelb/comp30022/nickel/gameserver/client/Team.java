package com.unimelb.comp30022.nickel.gameserver.client;

import java.util.ArrayList;

public class Team implements IRoom {
	
	private ArrayList<Player> members;
	private int size;
	private int score;
	
	public Team(){
		members = new ArrayList<Player>();
		size = 0;
		score = 0;
	}

	@Override
	public boolean addPlayer(Player player) {
		if (this.hasSpace()){
			members.add(player);
			return true;
		}
		return false;
	}

	@Override
	public boolean removePlayer(Player player) {
		if (members.contains(player)){
			members.remove(player);
			return true;
		}
		return false;
	}

	@Override
	public boolean hasSpace() {
		return (members.size() < (MAXSPACE/2));
	}
	
	public int getSize() {
		return size;
	}

	@Override
	public boolean hasPlayer(Player player) {
		return members.contains(player);
	}
	
	public void scored(){
		score++;
	}
	
	public int getScore(){
		return score;
	}

}
