/*************************************************************************
**
** This script is used to manage player's bahaviour
** in the game scene.
** 
** Author: Zequn Ma
** Also Maintained by Chi Peng
**
**************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	/// <summary>
	/// Player manager. 
	/// Handles fire Input and Beams.
	/// </summary>
	public class PlayerManager : Photon.PunBehaviour {


		[SerializeField] float m_MovingTurnSpeed = 180;
		[SerializeField] float m_StationaryTurnSpeed = 90;
		[SerializeField] float m_MoveSpeedMultiplier;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		private float m_normalSpeed = 1f;
		private float m_normalMass = 1f;
		public int team;

		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;

		Quaternion initRotation;
		Vector3 initPos;
		private int height_limit = 15;

		Rigidbody m_Rigidbody;
		Animator m_Animator;

		[Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
		public static GameObject LocalPlayerInstance;

		//True, when the user is firing
		bool IsFiring;

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		void Awake()
		{
			m_MoveSpeedMultiplier = m_normalSpeed;
			m_Rigidbody = GetComponent<Rigidbody> ();
			m_Animator = GetComponent<Animator> ();

			// #Important
			// used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
			if ( photonView.isMine)
			{
				PlayerManager.LocalPlayerInstance = this.gameObject;
				assignTeam ();
			}
			// #Critical
			// we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
			DontDestroyOnLoad(this.gameObject);

		}

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			
			CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

			if (_cameraWork!=null )
			{
				if ( photonView.isMine)
				{
					_cameraWork.OnStartFollowing();
				}
			}else{
				Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.",this);
			}

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

		}

		void assignTeam(){


			initPos = transform.position;
			initRotation = transform.rotation;

			if (initPos.x < 0) {
				team = 1;
				GameObject.FindGameObjectWithTag ("game").SendMessage ("enable_fire");
			} else {
				team = -1;
				GameObject.FindGameObjectWithTag ("game").SendMessage ("enable_ice");

			}
		}

		void resetPos(){
			m_Rigidbody.position = initPos;
			transform.rotation = initRotation;
			m_Rigidbody.velocity = new Vector3 (0, 0, 0);
		}

		void Update(){
			if (m_Rigidbody.transform.position.y < -height_limit) {
				resetPos ();
				if (team == 1) {
					GameObject.FindGameObjectWithTag ("oppoScore").SendMessage ("addScore", 1);
				} else {
					GameObject.FindGameObjectWithTag ("myScore").SendMessage ("addScore", 1);
				}
			}

			if (m_Rigidbody.transform.position.y > 2 * height_limit) {
				resetPos ();
			}
		}

		public void checkHeight(){
			if (m_Rigidbody.transform.position.y < -height_limit) {
				resetPos ();
				if (team == 1) {
					GameObject.FindGameObjectWithTag ("oppoScore").SendMessage ("addScore", 1);
				} else {
					GameObject.FindGameObjectWithTag ("myScore").SendMessage ("addScore", 1);
				}
			}

			if (m_Rigidbody.transform.position.y > 2 * height_limit) {
				resetPos ();
			}
		}

		public void updateMove(Vector3 move){
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);

			//slow down the control of joystick
			m_TurnAmount = (2 * m_TurnAmount ) / 3;

			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();


			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}

		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (Time.deltaTime > 0)
			{
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}

		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
			#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
			#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_Animator.applyRootMotion = true;
			}
			else
			{
				m_GroundNormal = Vector3.up;
				m_Animator.applyRootMotion = false;
			}
		}

		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)


			m_Animator.speed = m_MoveSpeedMultiplier;
		}

		public void boost(float time){
			if (time > 0) {
				m_MoveSpeedMultiplier = m_normalSpeed * 1.5f;
			} else {
				m_MoveSpeedMultiplier = m_normalSpeed;
			}
		}

		public void powerUp(float time){
			if (time > 0) {
				m_Rigidbody.mass = m_normalMass * 10;
			} else {
				m_Rigidbody.mass = m_normalMass;
			}
		}

		public void disable(){
			m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
		}

		public void enable(){
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		}

		public void OnCollisionEnter(Collision collision){
			if (collision.rigidbody != null && collision.gameObject.tag == "player") {
				Debug.LogWarning ("pushing really hard!");
				collision.rigidbody.AddForceAtPosition (m_Rigidbody.velocity * 80 * m_Rigidbody.mass, collision.transform.position);
			}
		}
	}