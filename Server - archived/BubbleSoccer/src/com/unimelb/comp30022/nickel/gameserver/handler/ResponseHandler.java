package com.unimelb.comp30022.nickel.gameserver.handler;

import org.json.JSONException;
import org.json.JSONObject;

import com.unimelb.comp30022.nickel.gameserver.application.BubbleSoccer;
import com.unimelb.comp30022.nickel.gameserver.message.IMessage;
import com.unimelb.comp30022.nickel.gameserver.message.ResponseMessage;

public class ResponseHandler implements IHandler{

	public static void handleMessage(IMessage message) {
		ResponseMessage handled = (ResponseMessage)message;
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

	private static void handleQuit(ResponseMessage handled) {
		JSONObject response = handled.getJson();
		try {
			BubbleSoccer.getPlayerById(response.getString("id")).sendToClient(handled.getMessage());
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}

	private static void handleMovement(ResponseMessage handled) {
		JSONObject response = handled.getJson();
		try {
			BubbleSoccer.getPlayerById(response.getString("id")).sendToClient(handled.getMessage());
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}

	private static void handleNewConnection(ResponseMessage handled) {
		JSONObject response = handled.getJson();
		try {
			BubbleSoccer.getPlayerById(response.getString("id")).sendToClient(handled.getMessage());
		} catch (JSONException e) {
			e.printStackTrace();
		}
	}

}
