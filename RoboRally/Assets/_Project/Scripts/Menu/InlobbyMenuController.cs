﻿using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InlobbyMenuController : MonoBehaviour {

	public Button StartButton;
	
	
	void Start() {
		StartButton.interactable = IngameData.IsHost;
	}


	void Update() {

	}

	public void Leave() {
		LobbyManager
	}
}
