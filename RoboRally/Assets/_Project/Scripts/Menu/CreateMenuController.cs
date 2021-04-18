using RoboRally.Controller;
using RoboRally.Menu.Elements;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace RoboRally.Menu {
	class CreateMenuController : MonoBehaviour {

		public TMP_InputField GameNameInput;
		public TMP_InputField PasswordInput;
		public TMP_InputField PlayerNameInput;
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
			if(!ValidateInputs())
				return;
			cm.CreateLobby(
				GameNameInput.text,
				PasswordInput.text,
				NamesSelector.index == 0,
				ComsSelector.index == 0,
				MaxPlayersSelector.index + 1,
				PlayerNameInput.text
			);
		}

		public bool ValidateInputs() {
			return 
				GameNameInput.text.Length >= 4 && 
				PasswordInput.text.Length >= 4 && 
				PlayerNameInput.text.Length >= 4;
		}
	}
}