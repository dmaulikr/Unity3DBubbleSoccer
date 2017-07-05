package com.unimelb.comp30022.nickel.gameserver.message;

import org.json.JSONException;
import org.json.JSONObject;

public abstract class IMessage {
	
	protected MessageType type;
	private JSONObject content;
	private String action;
	
	public IMessage(String data) throws JSONException{
		type = MessageType.DEFAULT;
		content = new JSONObject(data);
		action = content.getString("action");
	}
	
	public String getMessage(){
		return content.toString();
	}
	
	public JSONObject getJson(){
		return content;
	}
	
	public MessageType getType(){
		return type;
	}
	
	public String getAction(){
		return action;
	}
	
}
