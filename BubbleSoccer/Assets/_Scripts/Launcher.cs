/*************************************************************************
**
** This script is used to initialise connection to the network
** upon player joining a room.
** 
** Author: Zequn Ma
**
**************************************************************************/

using UnityEngine;

public class Launcher : Photon.PunBehaviour
{
		/// <summary>
		/// The PUN loglevel. 
		/// </summary>
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

		/// <summary>
		/// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
		/// </summary>   
		[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
		public byte MaxPlayersPerRoom = 4;

		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;

		public GameObject ballPrefab;

		/// <summary>
		/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
		/// </summary>
		string _gameVersion = "1";

		/// <summary>
		/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
		/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
		/// Typically this is used for the OnConnectedToMaster() callback.
		/// </summary>
		bool isConnecting;


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		void Awake()
		{
			PhotonNetwork.logLevel = Loglevel;

			// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.automaticallySyncScene = true;
		}


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			Connect();

		}

		/// <summary>
		/// Start the connection process. 
		/// - If already connected, we attempt joining a random room
		/// - if not yet connected, Connect this application instance to Photon Cloud Network
		/// </summary>
		public void Connect()
		{
			// keep track of the will to join a room
			isConnecting = true;

			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.connected)
			{
				PhotonNetwork.JoinRandomRoom();
			}else{
				PhotonNetwork.ConnectUsingSettings(_gameVersion);
			}
		}


		public override void OnConnectedToMaster()
		{

			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
			// try to join a potential existing room. If not we'll be called back with OnPhotonRandomJoinFailed()  
			if (isConnecting) {
				PhotonNetwork.JoinRandomRoom ();
			}

		}
			
		public override void OnDisconnectedFromPhoton()
		{
			Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");

		}

		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

			// failed to join a random room, create a new room.
			PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
			if (playerPrefab == null) {
				Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference.", this);
			} else {
				if (PlayerManager.LocalPlayerInstance==null)
				{
					// we're in a room. spawn a character for the local player at position based on number of player in the room
					Debug.Log(" "+PhotonNetwork.playerList.Length);
					switch (PhotonNetwork.playerList.Length) {
					case 1:
						PhotonNetwork.Instantiate (this.playerPrefab.name, new Vector3 (-10f, 0, 10f), Quaternion.Euler(0,135,0) , 0);
						break;
					case 2:
						PhotonNetwork.Instantiate (this.playerPrefab.name, new Vector3 (10f, 0, -10f), Quaternion.Euler(0,-45,0) , 0);
						break;
					case 3:
						PhotonNetwork.Instantiate (this.playerPrefab.name, new Vector3 (-10f, 0, -10f), Quaternion.Euler(0,45,0) , 0);
						break;
					case 4:
						PhotonNetwork.Instantiate (this.playerPrefab.name, new Vector3 (10f, 0, 10f), Quaternion.Euler(0,225,0) , 0);
						break;
					}
				}else{
					Debug.Log("Player prefab not found");
				}
			}

			if (ballPrefab != null && PhotonNetwork.playerList.Length == 1) {
				Debug.Log("We are Instantiating Ball from "+Application.loadedLevelName);
				PhotonNetwork.Instantiate (this.ballPrefab.name, new Vector3 (0f, 0.5f, 0f), Quaternion.identity, 0);

			}
		}

	}