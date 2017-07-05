package com.unimelb.comp30022.nickel.gameserver.message;

import org.json.JSONException;

public class EventMessage extends IMessage{
	
	public EventMessage(String data) throws JSONException {
		super(data);
		type = MessageType.EVENT;
	}

}
