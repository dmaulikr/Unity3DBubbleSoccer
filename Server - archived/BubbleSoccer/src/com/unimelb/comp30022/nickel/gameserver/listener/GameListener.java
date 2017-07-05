package com.unimelb.comp30022.nickel.gameserver.listener;

import java.io.IOException;

import org.json.JSONException;
import com.unimelb.comp30022.nickel.gameserver.client.Client;
import com.unimelb.comp30022.nickel.gameserver.message.EventMessage;
import com.unimelb.comp30022.nickel.gameserver.message.IMessage;

public class GameListener implements IListener{

	private Client client;
	
	public GameListener(Client client){
		this.client = client;
		new ReadThread().run();
	}
	
	@Override
	public void notifyClient(IMessage message) {
		client.onMessageRecieved(message);
	}

	private class ReadThread extends Thread{
		public void run() {
            super.run();
            byte[] bytes = new byte[1024];
            while (!client.getSocket().isClosed()){
            	try {
                    int data = client.getSocket().getInputStream().read(bytes);
                    if (data != -1){
                        String string = new String(bytes, 0, data);
                        notifyClient(new EventMessage(string));
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e){
                	e.printStackTrace();
                }
            }
		}
	}
	
}
