package com.unimelb.comp30022.nickel.gameserver.application;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.HashMap;

import com.unimelb.comp30022.nickel.gameserver.client.BubbleSoccerRoom;
import com.unimelb.comp30022.nickel.gameserver.client.Player;
import com.unimelb.comp30022.nickel.gameserver.message.IMessage;

public class BubbleSoccer {
	
	private static ArrayList<BubbleSoccerRoom> rooms;
	private static HashMap<String, Player> allPlayers;
	
	public static void main(String[] args){
		rooms = new ArrayList<BubbleSoccerRoom>();
		allPlayers = new HashMap<String, Player>();
		try {
            ServerSocket serverSocket = new ServerSocket(14723);
            while (true) {
                System.out.println("Wait client");
                Socket socket = serverSocket.accept();
                System.out.println("Client connected");
                Player newPlayer = new Player(socket);
                assignRoom(newPlayer);
                allPlayers.put(newPlayer.getId(), newPlayer);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
	}

	public static void broadcast(Player player, IMessage message) {
		for (BubbleSoccerRoom room : rooms){
			if (room.hasPlayer(player)){
				room.broadcast(player, message);
				return;
			}
		}
	}

	public static void onPlayerQuit(Player player) {
		for (BubbleSoccerRoom room : rooms){
			if (room.hasPlayer(player)){
				room.removePlayer(player);
				return;
			}
		}
	}
	
	private static void assignRoom(Player player){
		for (BubbleSoccerRoom room : rooms){
			if (room.addPlayer(player)) return;
		}
		BubbleSoccerRoom newRoom = new BubbleSoccerRoom(player);
		rooms.add(newRoom);
	}
	
	public static Player getPlayerById(String id){
		return allPlayers.get(id);
	}
	
	public static void gameEnd(BubbleSoccerRoom room){
		rooms.remove(room);
	}
	
}
