using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform targets;
	public float smoothing = 5f;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - targets.position;

	}

	void fixedUpdate() {
		Vector3 targetCamPos = targets.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos,smoothing * Time.deltaTime);
	}
}
