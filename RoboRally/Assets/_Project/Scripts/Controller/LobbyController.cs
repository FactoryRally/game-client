using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour {
	
	public GameObject LobbyItem;
	public GameObject LobbyContent;

	public Object[] Games;

	// Game item
	

	public void Start() {

	}
	
	public void Update() {

	}


	public void RefreshLobbys() {
		int i = 0;
		foreach(Object game in Games) {
			GameObject lobbyItem = Instantiate(LobbyItem, LobbyContent.transform);
			JoinLobbyElement jle = lobbyItem.GetComponent<JoinLobbyElement>();
			jle.GameID = i; // Set ID 
			jle.IsLocked = false; // Set if Locked
			i++;
		}
		
	}
}
