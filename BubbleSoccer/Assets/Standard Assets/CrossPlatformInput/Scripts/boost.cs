using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput{



	public class boost : MonoBehaviour {
		private float timer;
		private float cd = 6;
		private Button button;
		public float boost_time;
		public float boost_duration = 2;
		Text t;

		// Use this for initialization
		void Start () {
			timer = 0;
			boost_time = 0;
			button = GetComponent<Button>();
			button.onClick.AddListener (Boost);
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
				t.text = "Boost".ToString ();
				button.enabled = true;
			}
			if (boost_time > 0) {
				boost_time = boost_time - Time.deltaTime;
			}
		}

		void Boost(){
			timer = cd;
			boost_time = boost_duration;
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
