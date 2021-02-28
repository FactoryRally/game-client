using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;

public class JoinMenuController : MonoBehaviour {

	public GameObject LobbyItem;
	public GameObject LobbyContent;
	public GameObject LoadBox;
	public GameObject MessageBox;

	public LobbyManager lm;

	private List<JoinLobbyItem> items = new List<JoinLobbyItem>();


	public void Awake() {
		lm = GameObject.FindGameObjectWithTag("LobbyController").GetComponent<LobbyManager>();
	}

	public void Start() {

	}

	public void Update() {
		if(lm.state != LobbyCallState.LOADED && lm.state != LobbyCallState.NONE) {
			DeleteLobbyItems();
		}
		LoadBox.SetActive(lm.state == LobbyCallState.LOADING || lm.state == LobbyCallState.SCANNING);
		MessageBox.SetActive(lm.state == LobbyCallState.NO_GAMES_FOUND);
		if(lm.state == LobbyCallState.LOADED) {
			Reload();
			lm.state = LobbyCallState.NONE;
		}
	}


	public void Reload() {
		foreach((GameInfo game, int id) game in lm.games) {
			GameObject lobbyItem = Instantiate(LobbyItem, LobbyContent.transform);
			JoinLobbyItem jle = lobbyItem.GetComponent<JoinLobbyItem>();
			items.Add(jle);
			if(jle == null)
				continue;
			jle.GameID = game.id;
			jle.IsLocked = false;
			jle.GameNameText.text = game.game.Name;
			jle.PlayersAmountText.text = game.game.CurrentPlayers + "/" + game.game.MaxPlayers;
			jle.SetOnClick(lm);
			jle.UpdateGUI();
		}
	}

	private void DeleteLobbyItems() {
		foreach(JoinLobbyItem item in items) {
			Destroy(item.gameObject);
		}
		items.Clear();
	}

}
