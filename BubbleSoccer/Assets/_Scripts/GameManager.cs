/*************************************************************************
**
** This script is used to manage player leaving room and redirecting
** to the login screen.
** 
** Author: Zequn Ma
**
**************************************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class GameManager : Photon.PunBehaviour {

		/// <summary>
		/// Called when the local player left the room. We need to load the login scene.
		/// </summary>
		public void OnLeftRoom()
		{
			SceneManager.LoadScene(0);
		}

		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

		void OnLevelWasLoaded(int level)
		{
			// check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
			if (! Physics.Raycast(transform.position, -Vector3.up, 5f)) 
			{
				transform.position = new Vector3(0f,5f,0f);
			}
		}

		void LoadArena()
		{
			if ( ! PhotonNetwork.isMasterClient ) 
			{
				Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
			}
			Debug.Log( "PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount );
			PhotonNetwork.LoadLevel("Room for "+PhotonNetwork.room.playerCount);
		}


		public override void OnPhotonPlayerConnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerConnected() " + other.name ); // not seen if you're the player connecting


			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected

			}
		}


		public override void OnPhotonPlayerDisconnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerDisconnected() " + other.name ); // seen when other disconnects


			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected
			}
		}

}

