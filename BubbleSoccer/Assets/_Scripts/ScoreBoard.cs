using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void showImage(){
		image.enabled = true;
	}

	void hideImage(){
		image.enabled = false;
	}

}
