using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Networking;

public class JoinLobbyController : MonoBehaviour {

	public Object[] Games;

	// Game item
	

	public void Start() {

	}
	
	public void Update() {

	}

	public void RequestGames() {
		UnityWebRequest uwr = new UnityWebRequest();
		StartCoroutine(Http.Request("games", null,
				(x) => {
					uwr = x;
				}
			)
		);
	}
}
