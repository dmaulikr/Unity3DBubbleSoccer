using UnityEngine;
using System.Collections;

public class CollisionSound : MonoBehaviour {

	//volume:  0..1
	// magnitude

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag != "ground"){
			AudioSource sound = GetComponent<AudioSource> ();
			//if(!sound.isPlaying && col.relativeVelocity.magnitude >= 2){
			sound.volume = col.relativeVelocity.magnitude / 2;
			sound.Play ();
			//}
		}
	}

}
