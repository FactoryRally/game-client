using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoboRally.Menu.Elements {
	public class JoinLobbyItem : MonoBehaviour {

		public int GameID = 0;
		public string address = "localhost";
		public string password = "";
		public string playerName = "";
		public bool IsLocked = false;

		public Button JoinButton;
		public Image LockIcon;
		public TMP_Text GameNameText;
		public TMP_Text PlayersAmountText;

		public Sprite LockedImage;
		public Sprite UnlockedImage;


		public void Start() {

		}

		public void Update() {

		}


		public void UpdateGUI() {
			if(IsLocked) {
				LockIcon.sprite = LockedImage;
			} else {
				LockIcon.sprite = UnlockedImage;
			}
		}

		public void SetOnClick(JoinInputsMenuController joinInput) {
			if(JoinButton == null)
				return;
			JoinButton.onClick.AddListener(() => {
				joinInput.Show(IsLocked, address, GameID);
			});
		}

	}
}