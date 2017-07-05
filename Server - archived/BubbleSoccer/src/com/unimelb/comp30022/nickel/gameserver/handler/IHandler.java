package com.unimelb.comp30022.nickel.gameserver.handler;

import org.json.JSONException;

import com.unimelb.comp30022.nickel.gameserver.message.IMessage;

public interface IHandler {

	public static void handleMessage(IMessage message) throws JSONException {
	}
	
}
