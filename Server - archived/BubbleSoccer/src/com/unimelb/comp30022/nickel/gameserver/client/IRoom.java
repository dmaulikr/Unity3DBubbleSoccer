package com.unimelb.comp30022.nickel.gameserver.client;

public interface IRoom {

	public static int MAXSPACE = 6;
	
	public boolean addPlayer(Player player);
	public boolean removePlayer(Player player);
	public boolean hasPlayer(Player player);
	public boolean hasSpace();
	
}
