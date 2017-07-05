package com.unimelb.comp30022.nickel.gameserver.client;

import java.io.IOException;
import java.net.Socket;

public class Player extends Client{
	
	private GameObject avatar;
	private String username;

	public Player(Socket client) throws IOException {
		super(client);
		avatar = new GameObject();
		username = id;
	}
	
	public void update(Position position, Rotation rotation){
		avatar.updatePosition(position);
		avatar.updateRotation(rotation);
	}

}
