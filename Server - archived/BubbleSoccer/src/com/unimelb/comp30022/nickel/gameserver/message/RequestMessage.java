package com.unimelb.comp30022.nickel.gameserver.message;

import org.json.JSONException;

public class RequestMessage extends IMessage{
	
	public RequestMessage(String data) throws JSONException {
		super(data);
		type = MessageType.REQUEST;
	}

}
