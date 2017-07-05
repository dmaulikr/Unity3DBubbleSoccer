package com.unimelb.comp30022.nickel.gameserver.handler;

import com.unimelb.comp30022.nickel.gameserver.message.IMessage;
import com.unimelb.comp30022.nickel.gameserver.message.RequestMessage;

public class RequestHandler implements IHandler{

	public static void handleMessage(IMessage message){
		RequestMessage handled = (RequestMessage)message;
		switch(handled.getAction()){
		default:
			return;
		}
	}

}
