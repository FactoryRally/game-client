﻿using Newtonsoft.Json;
using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoboRally.Controller {
	public class InlobbyManager : MonoBehaviour {

		private static InlobbyManager _instance;
		public static InlobbyManager Instance { get { return _instance; } }
		public GameObject PlayerList;
		public GameObject PlayerCardPrefab;
		public Player PlayerMe = null;

		void Awake() {
			if(_instance != null && _instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				_instance = this;
			}
		}

		void Start() {
			HandleAllPlayers(IngameData.GameId);
		}

		void Update() {

		}


		public void LeaveLobby(int gameId, int playerId) {
			StartCoroutine(LeaveLobbyAsync(gameId, playerId));
		}

		public IEnumerator LeaveLobbyAsync(int gameId, int playerId) {
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
			PlayerMe = null;
			SceneManager.LoadScene("Menu_Main");
			FindObjectOfType<GlobalEventHandler>().StopListening();
		}

		public void StartGame(int gameId) {
			StartCoroutine(StartGameAsync(gameId));
		}

		public IEnumerator StartGameAsync(int gameId) {
			UnityWebRequest request = Http.CreatePut(
				$"games/{gameId}/actions",
				Http.Auth(new Dictionary<string, object>{
					{"action", ActionType.STARTGAME},
				})
			);
			return Http.Send(request);
		}

		public void GetPlayer(int gameId, int playerId, System.Action<Player> callBack = null) {
			StartCoroutine(GetPlayerAsync(gameId, playerId, callBack));
		}

		public IEnumerator GetPlayerAsync(int gameId, int playerId, System.Action<Player> callBack) {
			UnityWebRequest request = Http.CreateGet(
				$"games/{gameId}/players/{playerId}",
				Http.AuthOnlyParams
			);
			return Http.SendWithCallback(request, callBack);
		}

		public void GetPlayerIds(int gameId, System.Action<int[]> callBack = null) {
			StartCoroutine(GetPlayerIdsAsync(gameId, callBack));
		}

		public IEnumerator GetPlayerIdsAsync(int gameId, System.Action<int[]> callBack) {
			UnityWebRequest request = Http.CreateGet(
				$"games/{gameId}/players",
				Http.AuthOnlyParams
			);
			return Http.SendWithCallback(request, callBack);
		}

		public void OnGameStarted() {
			SceneManager.LoadScene("Game");
		}

		public void OnPlayerJoins(JoinEvent joinEvent) {
			Debug.Log("Player Joined");
			GetPlayer(
				IngameData.GameId, 
				joinEvent.JoinedId,
				(Player player) => {
					if(PlayerMe != null && PlayerMe.Equals(player))
						return;
					PlayerMe = player;
					IngameData.PlayerId = player.Id;
					if(joinEvent.Unjoin) {
						for(int i = 0; i < transform.childCount; i++) {
							GameObject obj = transform.GetChild(i).gameObject;
							if(obj.GetComponentInChildren<Text>().text.EndsWith("ID: " + joinEvent.JoinedId + ")"))
								Destroy(obj);
						}
					} else {
						GameObject obj = Instantiate(PlayerCardPrefab, PlayerList.transform);
						obj.GetComponentInChildren<TMP_Text>().text = player.DisplayName + " (ID: " + joinEvent.JoinedId + ")";
					}
				}
			);
		}

		public void HandleAllPlayers(int gameId) {
			GetPlayerIds(
				gameId,
				(int[] playerIds) => {
					if(playerIds == null)
						return;
					foreach(int playerId in playerIds) {
						GetPlayer(gameId, playerId, 
							(Player player) => {
								HandlePlayer(player);
							}
						);
					}
				}
			);
		}

		public void HandlePlayer(Player player) {
			if(player == null || player.Id == PlayerMe.Id)
				return;
			GameObject obj = Instantiate(PlayerCardPrefab, PlayerList.transform);
			obj.GetComponentInChildren<TMP_Text>().text = player.DisplayName + " (ID: " + player.Id + ")";
		}
	}
}