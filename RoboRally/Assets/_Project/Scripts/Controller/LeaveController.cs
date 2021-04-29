using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaveController : MonoBehaviour {

	private static LeaveController _instance;
	public static LeaveController Instance { get { return _instance; } }

	void Awake() {
		if(_instance != null && _instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
			DontDestroyOnLoad(this);
		}
	}

	public void LeaveGame() {
		StartCoroutine(LeaveGameAsync(IngameData.GameId, IngameData.PlayerId));
	}

	private IEnumerator LeaveGameAsync(int gameId, int playerId) {
		UnityWebRequest request = Http.CreateDelete(
			$"games/{gameId}/players/{playerId}",
			Http.AuthOnlyParams
		);
		yield return request.SendWebRequest();
		if(!request.isHttpError && request.downloadHandler != null) {
			Debug.Log("LeaveGame: " + request.downloadHandler.text);
		} else if(request.downloadHandler != null) {
			Debug.Log("LeaveGame: " + request.downloadHandler.text);
		}
	}
}
