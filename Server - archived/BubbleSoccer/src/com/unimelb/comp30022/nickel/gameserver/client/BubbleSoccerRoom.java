package com.unimelb.comp30022.nickel.gameserver.client;

import java.util.ArrayList;

import org.json.JSONException;
import org.json.JSONObject;

import com.unimelb.comp30022.nickel.gameserver.message.IMessage;

public class BubbleSoccerRoom implements IRoom {
	
	private Player host;
	private ArrayList<Player> players;
	private Team team1;
	private Team team2;
	private int remainingTime;
	
	public BubbleSoccerRoom(Player host){
		this.host = host;
		players = new ArrayList<Player>();
		players.add(host);
		team1 = new Team();
		team2 = new Team();
		remainingTime = 300;
		new GameThread().run();
	}

	public boolean changeHost(Player host, Player target){
		if (this.host == host) {
			this.host = target;
			return true;
		}
		return false;
	}
	
	@Override
	public boolean addPlayer(Player player) {
		if (this.hasSpace()){
			players.add(player);
			if (team1.getSize() <= team2.getSize()){
				team1.addPlayer(player);
			} else {
				team2.addPlayer(player);
			}
			return true;
		}
		return false;
	}

	@Override
	public boolean removePlayer(Player player) {
		if (team1.hasPlayer(player)) {
			team1.removePlayer(player);
		} else if (team2.hasPlayer(player)) {
			team2.removePlayer(player);
		} else return false;
		return true;
	}

	@Override
	public boolean hasSpace() {
		return (team1.hasSpace() || team2.hasSpace());
	}

	@Override
	public boolean hasPlayer(Player player) {
		return (team1.hasPlayer(player) || team2.hasPlayer(player));
	}

	public void broadcast(Player player, IMessage message) {
		for (Player p : players){
			p.sendToClient(message.getMessage());
		}
	}
	
	private void endGame(){
		for (Player player : players){
			JSONObject json = new JSONObject();
			try {
				json.put("id", player.getId());
				json.put("action", "quit");
				player.sendToClient(json.toString());
			} catch (JSONException e){
				e.printStackTrace();
			}
		}
	}
	
	private class GameThread extends Thread {
		public void run() {
            super.run();
            while (remainingTime != 0){
            	try {
					sleep(1000);
	            	remainingTime--;
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
            }
            endGame();
		}
	}

}
