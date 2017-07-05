package com.unimelb.comp30022.nickel.gameserver.client;

import java.io.IOException;
import java.net.Socket;
import java.util.UUID;

import com.unimelb.comp30022.nickel.gameserver.handler.EventHandler;
import com.unimelb.comp30022.nickel.gameserver.handler.RequestHandler;
import com.unimelb.comp30022.nickel.gameserver.handler.ResponseHandler;
import com.unimelb.comp30022.nickel.gameserver.listener.GameListener;
import com.unimelb.comp30022.nickel.gameserver.listener.IListener;
import com.unimelb.comp30022.nickel.gameserver.message.IMessage;

public class Client {

	private Socket client;
	private IListener listener;
	protected String id = UUID.randomUUID().toString();
	
	public Client(Socket client) throws IOException{
		this.client = client;
		this.listener = new GameListener(this);
	}
	
	public void onMessageRecieved(IMessage message){
		switch (message.getType()){
		case REQUEST:
			RequestHandler.handleMessage(message);
			break;
		case EVENT:
			EventHandler.handleMessage(message);
			break;
		case RESPONSE:
			ResponseHandler.handleMessage(message);
			break;
		default:
			break;
		}
	}
	
	public void sendToClient(String message){
		try {
            byte[] bytes = message.getBytes();
            byte[] bytesSize = intToByteArray(message.length());
            client.getOutputStream().write(bytesSize, 0, 4);
            client.getOutputStream().write(bytes, 0, bytes.length);
            client.getOutputStream().flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
	}
	
	private byte[] intToByteArray(int length) {
		byte[] ret = new byte[4];
        ret[0] = (byte) (length & 0xFF);
        ret[1] = (byte) ((length >> 8) & 0xFF);
        ret[2] = (byte) ((length >> 16) & 0xFF);
        ret[3] = (byte) ((length >> 24) & 0xFF);
        return ret;
	}
	
	public void disconnect(){
		try {
			client.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public Socket getSocket(){
		return client;
	}
	
	public boolean isClient(String id){
		return this.id == id;
	}
	
	public String getId(){
		return id;
	}
	
}
