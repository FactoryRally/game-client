using System.Diagnostics;
using UnityEngine;

namespace RoboRally.Menu {
	public class MainMenuController : MonoBehaviour {

		public string[] links;


		public void Awake() {

		}

		public void Start() {

		}

		public void Update() {

		}


		public void OpenLink(int index) {
			if(index < 0 || index >= links.Length)
				return;
			Application.OpenURL(links[index]);
		}

	}
}