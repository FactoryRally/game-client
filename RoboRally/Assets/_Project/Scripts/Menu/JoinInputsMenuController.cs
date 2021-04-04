using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinInputsMenuController : MonoBehaviour {

	public Button JoinButton;
	public TMP_InputField nameInput;
	public TMP_InputField passwordInput;
	private LobbyManager manager;


	private void Awake() {
		manager = GameObject.FindGameObjectWithTag("LobbyController").GetComponent<LobbyManager>();
	}

	void Start() {

	}

	void Update() {

	}


	public void Show(bool prot, string address, int gameId) {
		gameObject.SetActive(true);
		passwordInput.interactable = prot;
		nameInput.Select();
		JoinButton.onClick.RemoveAllListeners();
		JoinButton.onClick.AddListener(() => {
			if(prot) {
				manager.JoinLobby(address, gameId, passwordInput.text, nameInput.text);
			} else {
				manager.JoinLobby(address, gameId, null, nameInput.text);
			}
			Close();
		});
		MenuSwitcher.instance.MenuStack.Add(gameObject);
	}

	public void OnDisable() {
		nameInput.text = "";
		passwordInput.text = "";
	}

	public void Close() {
		MenuSwitcher.instance.MenuStack.Remove(gameObject);
		gameObject.SetActive(false);
	}
}
