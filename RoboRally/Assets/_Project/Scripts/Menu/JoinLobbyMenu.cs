using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JoinLobbyMenu : MonoBehaviour {

	public GameObject LobbyItem;
	public GameObject LobbyContent;

	public LobbyManager lm;

	void Awake() {
		lm = GetComponent<LobbyManager>();
	}

	void Start() {
		// Reload();
	}

	void Update() {

	}

	public void Reload() {
		lm.RequestGames(
			(response) => {
				DeleteLobbyItems();
				// Show Load
				int i = 0;
				foreach(Object game in lm.Games) {
					GameObject lobbyItem = Instantiate(LobbyItem, LobbyContent.transform);
					JoinLobbyItem jle = lobbyItem.GetComponent<JoinLobbyItem>();
					jle.GameID = i; // Set ID 
					jle.IsLocked = false; // Set if Locked
					i++;
				}
			}
		);
	}

	private void DeleteLobbyItems() {
		for(int childIndex = 0; childIndex < LobbyContent.transform.childCount; childIndex++) {
			Destroy(LobbyContent.transform.GetChild(childIndex).gameObject);
		}
	}
}
