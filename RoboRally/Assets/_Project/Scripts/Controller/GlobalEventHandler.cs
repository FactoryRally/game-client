using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoboRally.Utils;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.XR;
using static Tgm.Roborally.Api.Model.EventType;
using Action = System.Action;
using EventType = Tgm.Roborally.Api.Model.EventType;

#region EventClasses

[Serializable]
public class GenericUEvent : UnityEvent {}
[Serializable]
public class UMovementEvent : UnityEvent<MovementEvent> {}
[Serializable]
public class UPurchaseEvent : UnityEvent<PurchaseEvent> {}
[Serializable]
public class UShootEvent : UnityEvent<ShootEvent> {}
[Serializable]
public class UDrawCardEvent : UnityEvent<DrawCardEvent> {}
[Serializable]
public class UPickEvent : UnityEvent<RobotPickEvent> {}
[Serializable]
public class UChangeRegisterEvent : UnityEvent<ChangeRegisterEvent> {}
[Serializable]
public class UChangePhaseEvent : UnityEvent<GamePhaseChangedEvent> {}
[Serializable]
public class UJoinEvent : UnityEvent<JoinEvent> {}

#endregion

public class GlobalEventHandler : MonoBehaviour {
	private EventHandler                             _localHandler;
	private bool                                     FetchEvents;
	private GenericEvent                             lastEvent;
	private Queue<GenericEvent> unusedEvents = new Queue<GenericEvent>();
    public void HandleEvent(GenericEvent genericEvent) {
		if(genericEvent == null)
    				return;
		Debug.Log("[Event] : "+JsonConvert.SerializeObject(genericEvent));
		if (_localHandler == null) {
			unusedEvents.Enqueue(genericEvent);
			Debug.Log($"[Debug] Enqueue event for later use ({genericEvent.Type})");
		}
		else
			ExecuteEventListener(genericEvent);
    }

	private void ExecuteEventListener(GenericEvent genericEvent) {
		lastEvent = genericEvent;
		switch(lastEvent.Type) {
			case Movement:
				_localHandler.OnMovement.Invoke(Read<MovementEvent>());
				break;
			case UpgradePurchase:
				_localHandler.OnPurchase.Invoke(Read<PurchaseEvent>());
				break;
			case LazerShot:
				_localHandler.OnShoot.Invoke(Read<ShootEvent>());
				break;
			case MapCreated:
				_localHandler.OnMapCreate.Invoke();
				break;
			case TakeCardEvent:
				_localHandler.OnDrawCard.Invoke(Read<DrawCardEvent>());
				break;
			case LockIn:
				_localHandler.OnPickRobot.Invoke(Read<RobotPickEvent>());
				break;
			case ChangeRegister:
				_localHandler.OnChangeRegister.Invoke(Read<ChangeRegisterEvent>());
				break;
			case RandomCardDistribution:
				_localHandler.OnRandomDistrib.Invoke();
				break;
			case GameRoundPhaseChanged:
				_localHandler.OnChangePhase.Invoke(Read<GamePhaseChangedEvent>());
				break;
			case Join:
				_localHandler.OnJoin.Invoke(Read<JoinEvent>());
				break;
			case GameStart:
				_localHandler.OnGameStart.Invoke();
				break;
		}
	}

	private T Read<T>() where T : class => ((JObject) lastEvent.Data).ToObject<T>();
	

	private IEnumerator GetEventsAsync(int gameId) {
		while(FetchEvents) {
			UnityWebRequest request = Http.CreateGet(
				"games/" + gameId + "/events/head",
				Http.AuthOnlyParams
			);
			yield return Http.SendWithCallback<GenericEvent>(request,HandleEvent);
			yield return new WaitForSeconds(0.25f);
		}
	}
	public void StartListening(string address, int gameId) {
		FetchEvents = true;
		StartCoroutine(GetEventsAsync(gameId));
	}

	public void StopListening() {
		FetchEvents = false;
	}

	public void SetLocal(EventHandler eventHandler) {
		Debug.Log("Change Event Manager -> "+eventHandler);
		_localHandler = eventHandler;
		if (eventHandler != null) {
			Debug.Log("[DEBUG] Pop unused events");
			while (unusedEvents.Count > 0) {
				ExecuteEventListener(unusedEvents.Dequeue());
			}
		}
	}
}
