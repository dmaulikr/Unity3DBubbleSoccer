package com.unimelb.comp30022.nickel.gameserver.client;

public class GameObject {

	private Position position;
	private Rotation rotation;
	
	public GameObject(){
		position = new Position();
		rotation = new Rotation();
	}
	
	public void updatePosition(String x, String y, String z){
		position.update(x, y, z);
	}
	
	public void updateRotation(String w, String x, String y, String z){
		rotation.update(w, x, y, z);
	}
	
	public Position getPosition(){
		return position;
	}
	
	public Rotation getRotation(){
		return rotation;
	}

	public void updatePosition(Position position) {
		this.position = position;
	}
	
	public void updateRotation(Rotation rotation) {
		this.rotation = rotation;
	}
	
}
