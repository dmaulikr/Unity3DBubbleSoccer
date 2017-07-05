using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class oppoScore : MonoBehaviour {

	int score = 0;
	Text oppo_score;

	// Use this for initialization
	void Start () {
		oppo_score = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void addScore(int points){
		score = score + points;
		oppo_score.text = score.ToString();
	}
}
