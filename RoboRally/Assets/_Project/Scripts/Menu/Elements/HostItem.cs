using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoboRally.Menu.Elements {
	public class HostItem : MonoBehaviour {

		public string ip;

		public Button RemoveButton;
		public TMP_Text Text;

		void Start() {
			Text.text = ip;
		}

		void Update() {

		}

		public void SetOnClick(HostMenuController hmc) {
			RemoveButton.onClick.AddListener(() => {
				hmc.RemoveHost(ip);
			});
		}
	}
}