using Newtonsoft.Json;
using RoboRally.Objects;
using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Controller {
	public partial class GameController : MonoBehaviour {

		private static GameController _instance;
		public static GameController Instance { get { return _instance; } }

		public bool SendEvents = true;

		void Start() {
			if(_instance != null && _instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				_instance = this;
			}
			GetEvents(IngameData.Address, IngameData.ID);
		}

		void Update() {

		}

		public void GetEvents(string address, int gameId) {
			StartCoroutine(GetEventsAsync(address, gameId));
		}

		public IEnumerator GetEventsAsync(string address, int gameId) {
			while(SendEvents) {
				UnityWebRequest request = Http.CreateGet(
					address,
					"games/" + gameId + "/events/head",
					"pat=" + UnityWebRequest.EscapeURL(IngameData.JoinData.Pat)
				);
				yield return request.SendWebRequest();
				if(!request.isHttpError && request.downloadHandler != null) {
					Debug.Log(request.downloadHandler.text);
					GenericEvent genericEvent = JsonConvert.DeserializeObject<GenericEvent>(request.downloadHandler.text);
					HandleEvent(genericEvent);
				} else if(request.downloadHandler != null) {
					Debug.Log("GetEvent: No events to fetch");
				}
				yield return new WaitForSeconds(0.25f);
			}
		}

		public void GetMap(string address, int gameId, System.Action action) {
			if(IngameData.JoinData == null)
				return;
			StartCoroutine(GetMapAsync(address, gameId, action));
		}

		public IEnumerator GetMapAsync(string address, int gameId, System.Action action) {
			UnityWebRequest request = Http.CreateGet(
				address,
				"games/" + gameId + "/map/",
				"pat=" + UnityWebRequest.EscapeURL(IngameData.JoinData.Pat)
			);
			yield return request.SendWebRequest();
			if(!request.isHttpError && request.downloadHandler != null) {
				Debug.Log("GetMap: " + request.downloadHandler.text);
				IngameData.SelectedMap = JsonConvert.DeserializeObject<Map>(request.downloadHandler.text);
				action();
			} else if(request.downloadHandler != null) {
				Debug.Log("GetMap: " + request.downloadHandler.text);
			}
		}

		public void HandleEvent(GenericEvent genericEvent) {
			if(genericEvent == null)
				return;
			Debug.Log("EventType: " + genericEvent.Type + " - " + (int) genericEvent.Type);
			switch((int) genericEvent.Type) {
				case 1:
					MovementEvent moveEvent = genericEvent.Data as MovementEvent;
					HandleMoveEvent(moveEvent);
					break;
				case 2:
					PurchaseEvent purchaseEvent = genericEvent.Data as PurchaseEvent;
					break;
				case 3:
					break;
				case 4:
					ShootEvent shootEvent = genericEvent.Data as ShootEvent;
					break;
				case 5:
					break;
				case 6:
					break;
				case 7:
					break;
				case 8:
					break;
				case 9:
					break;
				case 10:
					break;
				case 11:
					break;
				case 12:
					break;
				case 13:
					break;
				case 14:
					break;
				case 15:
					PauseEvent pauseEvent = genericEvent.Data as PauseEvent;
					break;
				case 16:
					break;
				case 17:
					break;
				case 18:
					break;
				case 19:
					PushEvent pushEvent = genericEvent.Data as PushEvent;
					break;
				case 20:
					break;
				case 21:
					break;
				case 22:
					break;
				case 23:
					break;
				case 24:
					break;
				case 25:
					break;
				case 26:
					ShutdownEvent shutdownEvent = genericEvent.Data as ShutdownEvent;
					break;
				case 27:
					break;
				case 28:
					break;
				case 29:
					break;
				case 30:
					break;
				case 31:
					HandleMapCreateEvent();
					break;
				case 32:
					break;
			}
		}


		public void HandleMoveEvent(MovementEvent moveEvent) {

		}

		public void HandleMapCreateEvent() {
			GetMap(IngameData.Address, IngameData.ID, () => {
				MapBuilder.Instance.SelectedMap = IngameData.SelectedMap;
				MapBuilder.Instance.BuildMap();
			});
		}
	}
}