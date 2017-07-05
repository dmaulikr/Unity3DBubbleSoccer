package com.unimelb.comp30022.nickel.gameserver.handler;

import org.json.JSONException;
import org.json.JSONObject;

import com.unimelb.comp30022.nickel.gameserver.application.BubbleSoccer;
import com.unimelb.comp30022.nickel.gameserver.client.Player;
import com.unimelb.comp30022.nickel.gameserver.client.Position;
import com.unimelb.comp30022.nickel.gameserver.client.Rotation;
import com.unimelb.comp30022.nickel.gameserver.message.EventMessage;
import com.unimelb.comp30022.nickel.gameserver.message.IMessage;
import com.unimelb.comp30022.nickel.gameserver.message.ResponseMessage;

public class EventHandler implements IHandler{

	public static void handleMessage(IMessage message) {
		EventMessage handled = (EventMessage)message;
		switch (handled.getAction()){
		case "newconnection":
			handleNewConnection(handled);
			break;
		case "move":
			handleMovement(handled);
			break;
		case "quit":
			handleQuit(handled);
			break;
		default:
			return;	
		}
	}

	private static void handleQuit(EventMessage handled) {
		JSONObject event = handled.getJson();
		try {
			ResponseMessage response = new ResponseMessage(handled.getMessage());
			BubbleSoccer.getPlayerById(event.getString("id")).onMessageRecieved(response);
		} catch (JSONException e){
			e.printStackTrace();
		}
	}

	private static void handleMovement(EventMessage handled) {
		JSONObject event = handled.getJson();
		try {
			Player player = BubbleSoccer.getPlayerById(event.getString("id"));
			ResponseMessage response = new ResponseMessage(handled.getMessage());
			Position newPosition = new Position(event.getJSONObject("position"));
			Rotation newRotation = new Rotation(event.getJSONObject("rotation"));
			player.update(newPosition, newRotation);
			player.onMessageRecieved(response);
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}

	private static void handleNewConnection(EventMessage handled) {
		JSONObject event = handled.getJson();
		try {
			ResponseMessage response = new ResponseMessage(handled.getMessage());
			BubbleSoccer.getPlayerById(event.getString("id")).onMessageRecieved(response);
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}

}
