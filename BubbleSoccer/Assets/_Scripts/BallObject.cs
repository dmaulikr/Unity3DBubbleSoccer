using UnityEngine;
using System.Collections;

public class BallObject : MonoBehaviour {
	float timer = 3;
	int goal = 0;
	int teamScored = 0;
	Vector3 pos;
	Rigidbody m_Rigidbody;
	private int goal_post_out = 50;
	private int goal_post_in = 45;
	private int goal_post_left = -10;
	private int goal_post_right = 10;
	private int height_limit = 15;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		detectGoal ();
		reset ();
	}



	private void reset(){
		if(m_Rigidbody.transform.position.y < -height_limit) {
			resetPos ();
		}
	}

	private void resetPos(){
		transform.position = new Vector3(0,1,0);
		m_Rigidbody.velocity = new Vector3 (0, 0, 0);
		m_Rigidbody.freezeRotation = true;
		m_Rigidbody.freezeRotation = false;
	}

	private void detectGoal(){
		pos = transform.position;
		if (pos.x > goal_post_in && pos.x < goal_post_out && pos.z < goal_post_right && pos.z > goal_post_left) {
			goal = 1;
			teamScored = 1;
		} else if (pos.x < -goal_post_in && pos.x > -goal_post_out && pos.z < goal_post_right && pos.z > goal_post_left) {
			goal = 1;
			teamScored = -1;
		}
		if (goal == 1) {
			timer = timer - Time.deltaTime;
			GameObject.FindGameObjectWithTag ("scoreBoard").SendMessage ("showImage");
		}
		if (timer <= 0) {
			goal = 0;
			if (teamScored == 1) {
				GameObject.FindGameObjectWithTag ("myScore").SendMessage ("addScore", 3);
			} else {
				GameObject.FindGameObjectWithTag ("oppoScore").SendMessage ("addScore", 3);
			}
			GameObject.FindGameObjectWithTag ("scoreBoard").SendMessage ("hideImage");
			teamScored = 0;
			timer = 3;
			resetPos ();

			GameObject.FindGameObjectWithTag ("game").SendMessage ("restart");
		}
	}
}
