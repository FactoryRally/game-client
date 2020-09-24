using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinLobbyMenu : MonoBehaviour {

	public GameObject LobbyItem;
	public GameObject LobbyContent;

	public JoinLobbyController jlc;

	void Awake() {
		jlc = GetComponent<JoinLobbyController>();
	}

	void Start() {

	}

	void Update() {

	}


	public void ReloadButton() {
		jlc.RequestGames();
		int i = 0;
		foreach(Object game in jlc.Games) {
			GameObject lobbyItem = Instantiate(LobbyItem, LobbyContent.transform);
			JoinLobbyItem jle = lobbyItem.GetComponent<JoinLobbyItem>();
			jle.GameID = i; // Set ID 
			jle.IsLocked = false; // Set if Locked
			i++;
		}

	}
}
