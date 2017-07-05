using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
	[RequireComponent(typeof (Joystick))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;                    // the world-relative desired move direction, calculated from the camForward and user input.
		public Joystick joystick;
		public boost boost_btn;
		public strike strike_btn;
		public RawImage boost_bar;
		public RawImage strike_bar;
		public Text boost_text;
		public Text strike_text;
		private float bar_width = 600;
        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
			joystick = GameObject.FindGameObjectWithTag ("joystick").GetComponent<Joystick> ();
			boost_btn = GameObject.FindGameObjectWithTag ("boost").GetComponent<boost> ();
			strike_btn = GameObject.FindGameObjectWithTag ("strike").GetComponent<strike> ();
			boost_bar = GameObject.Find ("boost bar").GetComponent<RawImage> ();
			strike_bar = GameObject.Find ("strike bar").GetComponent<RawImage> ();
			boost_text = GameObject.Find ("boost text").GetComponent<Text> ();
			strike_text = GameObject.Find ("strike text").GetComponent<Text> ();

        }

		void Update(){
			float new_width;
			if (boost_btn.boost_time > 0) {
				boost_bar.enabled = true;
				boost_text.enabled = true;
				new_width = bar_width * (boost_btn.boost_time / boost_btn.boost_duration);
				var boostBarRect = boost_bar.transform as RectTransform;
				boostBarRect.sizeDelta = new Vector2 (new_width, boostBarRect.sizeDelta.y);
			} else {
				boost_text.enabled = false;
				boost_bar.enabled = false;
			}

			if (strike_btn.strike_time > 0) {
				strike_bar.enabled = true;
				strike_text.enabled = true;
				new_width = bar_width * (strike_btn.strike_time / strike_btn.strike_duration);
				var strikeBarRect = strike_bar.transform as RectTransform;
				strikeBarRect.sizeDelta = new Vector2 (new_width, strikeBarRect.sizeDelta.y);
			} else {
				strike_bar.enabled = false;
				strike_text.enabled = false;
			}
		}
			
		// Fixed update is called in sync with physics
		private void FixedUpdate() 
        {
            // read inputs
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");

			float h = joystick.getX ();
			float v = joystick.getY ();

			//float h = joystick.Horizontal();
			//float v = joystick.Vertical();
            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
				m_Move = v * Vector3.forward + h * Vector3.right;
            }

            // pass all parameters to the character control script
			m_Character.Move(m_Move);
			m_Character.updateStatus (boost_btn.boost_time, strike_btn.strike_time);
        }
    }
}
