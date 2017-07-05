package com.unimelb.comp30022.nickel.gameserver.client;

import org.json.JSONException;
import org.json.JSONObject;

public class Position {

	private String x;
	private String y;
	private String z;
	
	public Position(){
		x = "0";
		y = "0";
		z = "0";
	}
	
	public Position(String x, String y, String z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public Position(JSONObject json){
		try {
			this.x = json.getString("x");
			this.y = json.getString("y");
			this.z = json.getString("z");
		} catch (JSONException e){
			e.printStackTrace();
		}
	}
	
	public void update(String x, String y, String z){
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public JSONObject toJson() throws JSONException{
		JSONObject json = new JSONObject();
		json.put("x", x);
		json.put("y", y);
		json.put("z", z);
		return json;
	}

}
