using RoboRally.Controller;
using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoboRally.Menu {
	public class InlobbyMenuController : MonoBehaviour {

		public Button StartButton;


		void Start() {
			StartButton.interactable = IngameData.IsHost;
		}


		void Update() {

		}

		public void Leave() {
			InlobbyManager.Instance.LeaveLobby(IngameData.ID, IngameData.JoinData.Id);
		}

		public void StartGame() {
			InlobbyManager.Instance.StartGame(IngameData.ID);
		}
	}
}
