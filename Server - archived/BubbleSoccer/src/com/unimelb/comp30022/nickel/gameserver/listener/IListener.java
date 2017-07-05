package com.unimelb.comp30022.nickel.gameserver.listener;

import com.unimelb.comp30022.nickel.gameserver.message.IMessage;

public interface IListener {

	public void notifyClient(IMessage message);
	
}
