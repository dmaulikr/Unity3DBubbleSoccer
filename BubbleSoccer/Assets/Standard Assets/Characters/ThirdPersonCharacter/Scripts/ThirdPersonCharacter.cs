using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class ThirdPersonCharacter : MonoBehaviour
	{

		void Start()
		{
		}

		void Update(){
			
		}

		public void Move(Vector3 move)
		{
			this.SendMessage ("updateMove", move);


		}
			

		public void updateStatus(float boost_time, float strike_time){
			SendMessage ("boost", boost_time);
			SendMessage ("powerUp", strike_time);
		}

		void OnCollisionEnter(Collision collision){
				
		}


		void stiff(){
			
		}


	}
}
