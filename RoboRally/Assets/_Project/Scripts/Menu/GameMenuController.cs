using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoboRally.Menu {
	public class GameMenuController : MonoBehaviour {

		public GameObject GameMenu;
		public GameObject Settings;

		public bool InSettings = false;

		void Start() {

		}


		void Update() {
			if(Input.GetKeyDown(KeyCode.Escape)) {
				if(InSettings) {
					CloseSettings();
				} else {
					BackToGame();
				}
			}
		}


		public void BackToGame() {
			SceneManager.UnloadSceneAsync("Menu_Game");
		}

		public void OpenSettings() {
			InSettings = true;
			GameMenu.SetActive(false);
			Settings.SetActive(true);
		}

		public void CloseSettings() {
			InSettings = false;
			Settings.SetActive(false);
			GameMenu.SetActive(true);
		}

		public void LeaveGame() {
			SceneManager.LoadScene("Menu_Main");
		}
	}
}