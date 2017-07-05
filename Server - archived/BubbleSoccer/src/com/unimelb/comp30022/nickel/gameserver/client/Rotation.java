package com.unimelb.comp30022.nickel.gameserver.client;

import org.json.JSONException;
import org.json.JSONObject;

public class Rotation {

	private String w;
	private String x;
	private String y;
	private String z;
	
	public Rotation(){
		w = "0";
		x = "0";
		y = "0";
		z = "0";
	}
	
	public Rotation(String w, String x, String y, String z) {
		this.w = w;
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public Rotation(JSONObject json){
		try {
			this.w = json.getString("w");
			this.x = json.getString("x");
			this.y = json.getString("y");
			this.z = json.getString("z");
		} catch (JSONException e){
			e.printStackTrace();
		}
	}
	
	public void update(String w, String x, String y, String z){
		this.w = w;
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public JSONObject toJson() throws JSONException{
		JSONObject json = new JSONObject();
		json.put("w", w);
		json.put("x", x);
		json.put("y", y);
		json.put("z", z);
		return json;
	}
	
}
