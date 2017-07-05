using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	private int num_players;
	private float freezeTime;
	private int waiting;
	private float game_time;
	public Text game_time_text;
	private bool start;
	private Player p;
	public RawImage team_fire_icon;
	public RawImage team_ice_icon;
	private bool quiting;

	// Use this for initialization
	void Start () {
		p = new Player ();
		p.LoadPlayer();
		Debug.LogWarning (p.get_username ());
		game_time = 120f;
		num_players = 0;
		restart ();
	}
	
	// Update is called once per frame
	void Update () {
		num_players = PhotonNetwork.playerList.Length;
		if (game_time <= -5) {
			exit ();
		}
		if (quiting == false && start == true && num_players != 2 && num_players != 4) {
			quiting = true;
			game_time = 0;
			GameObject.FindGameObjectWithTag ("freezeTimer").SendMessage ("playerQuit");
		}
		if (quiting == false) {
			updateTime ();
//			foreach (GameObject go in GameObject.FindGameObjectsWithTag("player")) {
//				go.SendMessage ("checkHeight");
//			}
		}

		if (freezeTime <= 0 && start == false) {
			start = true;
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("player")) {
				go.SendMessage ("enable");
			}
			waiting = 0;
			GameObject.FindGameObjectWithTag ("freezeTimer").SendMessage ("disable");
			GameObject.FindGameObjectWithTag ("boost").SendMessage ("enable");
			GameObject.FindGameObjectWithTag ("strike").SendMessage ("enable");

		}

		if (start == true && game_time > -5) {
			game_time = game_time - Time.deltaTime;
		}

		if (waiting == 1) {
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("player")) {
				go.SendMessage ("disable");
			}
			if (num_players == 4 || num_players == 2) {
				freezeTime = freezeTime - Time.deltaTime;
				GameObject.FindGameObjectWithTag ("freezeTimer").SendMessage ("updateTime", freezeTime);
			}
			GameObject.FindGameObjectWithTag ("boost").SendMessage ("disable");
			GameObject.FindGameObjectWithTag ("strike").SendMessage ("disable");
		}




	}

	public void restart(){
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("player")) {
			player.SendMessage ("resetPos");
		}
		start = false;
		waiting = 1;
		freezeTime = 6;
		quiting = false;
		GameObject.FindGameObjectWithTag ("freezeTimer").SendMessage ("enable");
	}

	private void updateTime(){
		if (game_time > 0) {
			int min = (int)game_time / 60;
			int second = (int)game_time - min * 60;
			game_time_text.text = min.ToString () + ":" + second.ToString ("00");
		} else {
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("player")) {
				go.SendMessage ("disable");
			}
			game_time_text.text = "End";
			GameObject.FindGameObjectWithTag ("freezeTimer").SendMessage ("endGame");

		}
	}

	public void enable_fire(){
		Debug.LogWarning ("fire");
		team_fire_icon.enabled = true;
	}

	public void enable_ice(){
		Debug.LogWarning ("ice");
		team_ice_icon.enabled = true;
	}

	private void exit(){
		PhotonNetwork.Disconnect ();
		p.update_rank ((p.get_rank() + 1));
		ExitGames ("_scene/StartMenu");
	}


	public void ExitGames(string name){
		PhotonNetwork.Disconnect ();


		Application.LoadLevel(name);
	}
}
