using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboRally.Utils;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


public class EventHandler : MonoBehaviour {
    public  GenericUEvent        OnMapCreate      = new GenericUEvent();
	public  UMovementEvent       OnMovement       = new UMovementEvent();
	public  UShootEvent          OnShoot          = new UShootEvent();
	public  UPurchaseEvent       OnPurchase       = new UPurchaseEvent();
	public  UDrawCardEvent       OnDrawCard       = new UDrawCardEvent();
	public  UPickEvent           OnPickRobot      = new UPickEvent();
	public  UChangeRegisterEvent OnChangeRegister = new UChangeRegisterEvent();
	public  GenericUEvent        OnRandomDistrib  = new GenericUEvent();
	public  UChangePhaseEvent    OnChangePhase    = new UChangePhaseEvent();
	public  UJoinEvent           OnJoin           = new UJoinEvent();
	private GlobalEventHandler   _globalEventHandler;
	public  GenericUEvent               OnGameStart = new GenericUEvent();

	// Start is called before the first frame update
    void Start() {
		_globalEventHandler = FindObjectOfType<GlobalEventHandler>();
		_globalEventHandler.SetLocal(this);
	}

	private void OnDestroy() {
		_globalEventHandler.SetLocal(null);
	}
}
