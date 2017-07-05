using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class myScore : MonoBehaviour {

	int score = 0;
	Text my_score;

	// Use this for initialization
	void Start () {
		my_score = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void addScore(int points){
		score = score + points;
		my_score.text = score.ToString();
	}
}
