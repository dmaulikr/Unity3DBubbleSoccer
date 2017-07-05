using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CubeController : MonoBehaviour {

	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);
		if (Input.anyKey) {
		}
	}
}
