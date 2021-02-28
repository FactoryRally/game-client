using UnityEngine;

public class JoinMenuController : MonoBehaviour {

	public GameObject LobbyItem;
	public GameObject LobbyContent;
	public GameObject LoadBox;
	public GameObject MessageBox;

	public LobbyManager lm;


	public void Awake() {
		lm = GameObject.FindGameObjectWithTag("LobbyController").GetComponent<LobbyManager>();
	}

	public void Start() {

	}

	public void Update() {
		if(lm.state == LobbyCallState.SCANNED) {
			DeleteLobbyItems();
		}
		LoadBox.SetActive(lm.state == LobbyCallState.LOADING);
		MessageBox.SetActive(lm.state == LobbyCallState.NO_GAMES_FOUND);
		if(lm.state == LobbyCallState.LOADED) {
			// Reload();
			lm.state = LobbyCallState.NONE;
		}
	}


	public void Reload() {
		int i = 0;
		foreach(string game in lm.games) {
			GameObject lobbyItem = Instantiate(LobbyItem, LobbyContent.transform);
			JoinLobbyItem jle = lobbyItem.GetComponent<JoinLobbyItem>();
			jle.GameID = i; // Set ID 
			jle.IsLocked = false; // Set if Locked
			i++;
		}
	}

	private void DeleteLobbyItems() {
		for(int childIndex = 0; childIndex < LobbyContent.transform.childCount; childIndex++) {
			Destroy(LobbyContent.transform.GetChild(childIndex).gameObject);
		}
	}

}
