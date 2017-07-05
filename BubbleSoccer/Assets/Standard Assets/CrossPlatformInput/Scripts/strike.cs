using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput{
	public class strike : MonoBehaviour {
		private float timer;
		private float cd = 8;
		private Button button;
		public float strike_time;
		public float strike_duration = 3;
		Text t;

		// Use this for initialization
		void Start () {
			timer = 0;
			strike_time = 0;
			button = GetComponent<Button>();
			button.onClick.AddListener (Strike);
			t = GetComponentInChildren<Text>();
		}

		// Update is called once per frame
		void Update () {
			float percentage = 0.4f + (1 - (timer / cd)) * 0.6f;
			Color c = button.image.color;
			c.a = percentage;
			button.image.color = c;
			if (timer > 0) {
				t.text = ((int)timer+1).ToString ();
				timer = timer - Time.deltaTime;
			} else {
				t.text = "Strike".ToString ();
				button.enabled = true;
			}
			if (strike_time > 0) {
				strike_time = strike_time - Time.deltaTime;
			}
		}

		void Strike(){
			timer = cd;
			strike_time = strike_duration;
			button.enabled = false;
		}

		public void disable(){
			button.enabled = false;
		}

		public void enable(){
			button.enabled = true;
		}
	}
}
