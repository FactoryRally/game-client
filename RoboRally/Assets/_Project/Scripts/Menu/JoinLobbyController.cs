using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Networking;

public class JoinLobbyController : MonoBehaviour {

	public UnityEngine.Object[] Games;

	// Game item


	public void Start() {

	}

	public void Update() {

	}

	public void RequestGames(Action<string[]> action) {
		StartCoroutine(RequestGamesAsync(action));
	}
	public IEnumerator RequestGamesAsync(Action<string[]> callback) {
		UnityWebRequest request = Http.CreateRequest("games", null);
		yield return request.SendWebRequest();
		int[] gameIds = JsonConvert.DeserializeObject<int[]>(request.downloadHandler.text);
		string[] games = new string[gameIds.Length];
		for(int i = 0; i < gameIds.Length; i++) {
			request = Http.CreateRequest("games/" + gameIds[i] + "/status", null);
			yield return request.SendWebRequest();
			if(request.responseCode == 200)
				games[i] = request.downloadHandler.text;
			Debug.Log(request.downloadHandler.text);
		}
		callback(games);
	}
}
