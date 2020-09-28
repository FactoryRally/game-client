using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
		jlc.RequestGames(
			(response) => {
				DeleteLobbyItems();
				int i = 0;
				foreach(UnityEngine.Object game in jlc.Games) {
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
