using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class freezeTimer : MonoBehaviour {
	Text t;

	// Use this for initialization
	void Start () {
		t = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateTime(float time){
		int time_t = (int)time;
		System.String s = "";
		if (time_t > 0) {
			s = time_t.ToString();
		} else {
			s = "GO";
		}
		t.text = s;

	}

	public void enable(){
		this.enabled = true;
	}

	public void disable(){
		t.text = "";
		this.enabled = false;
	}

	public void endGame(){
		t.text = "Returning to Main Menu";
		this.enabled = true;
	}

	public void playerQuit(){
		t.text = "Not Enough Players";
		this.enabled = true;
	}
}
