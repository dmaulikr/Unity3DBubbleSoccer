package com.unimelb.comp30022.nickel.gameserver.message;

import org.json.JSONException;

public class ResponseMessage extends IMessage{

	public ResponseMessage(String data) throws JSONException {
		super(data);
		type = MessageType.RESPONSE;
	}

}
