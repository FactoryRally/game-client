using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

class CreateMenuController : MonoBehaviour {

	public TMP_InputField GameNameInput;
	public TMP_InputField ServerInput;
	public TMP_InputField PasswordInput;
	public ButtonSelector NamesSelector;
	public ButtonSelector ComsSelector;
	public ButtonSelector MaxPlayersSelector;

	public CreateManager cm;


	public void Awake() {
		cm = GameObject.FindGameObjectWithTag("LobbyController").GetComponent<CreateManager>();
	}

	public void Start() {

	}

	public void Update() {

	}

	public void Create() {
		string address = LobbyManager.GetLocalIPAddress();
		cm.CreateLobby(
			GameNameInput.text,
			PasswordInput.text,
			NamesSelector.index == 0,
			ComsSelector.index == 0,
			MaxPlayersSelector.index + 1
		); ;
	}
}
