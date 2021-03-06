﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : MonoBehaviour {

	public List<string> localAddresses = new List<string>();

	public List<string> hostAddresses = new List<string>();
	public List<(GameInfo game, string address, int id)> games = new List<(GameInfo game, string address, int id)>();

	public LobbyCallState state = LobbyCallState.NONE;
	private int maxSubnet = 24;
	public AddressFinder af;


	public void Awake() {
		af = new AddressFinder(this);
		GetGames(true);
	}

	public void Start() {

	}

	public void Update() {
		if(state == LobbyCallState.SCANNED) {
			List<string> addresses = new List<string>(localAddresses);
			addresses.AddRange(hostAddresses);
			RequestGames(addresses);
		}
	}


	public bool AddHost(string text) {
		if(hostAddresses.Contains(text))
			return false;
		IPAddress address;
		if(IPAddress.TryParse(text, out address)) {
			if(address.AddressFamily == AddressFamily.InterNetwork) {
				hostAddresses.Add(text);
				return true;
			}
		}
		return false;
	}

	public void RemoveHost(string text) {
		hostAddresses.Remove(text);
	}

	public void GetGames(bool reloadHosts = false) {
		if(state == LobbyCallState.LOADING || state == LobbyCallState.SCANNING)
			return;
		this.games = new List<(GameInfo game, string address, int id)>();
		if(reloadHosts) {
			GetLocalAddresses();
		} else {
			state = LobbyCallState.SCANNED;
		}
	}

	public void GetLocalAddresses() {
		if(state != LobbyCallState.NONE)
			return;
		state = LobbyCallState.SCANNING;
		List<IPSegment> IPSegments = GetInterfaces(false);
		af = new AddressFinder(this);
		af.Scan(
			IPSegments,
			(addresses) => {
				localAddresses = addresses;
				state = LobbyCallState.SCANNED;
			}
		);
	}

	public static string GetLocalIPAddress() {
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach(var ip in host.AddressList) {
			if(ip.AddressFamily == AddressFamily.InterNetwork) {
				return ip.ToString();
			}
		}
		throw new Exception("No network adapters with an IPv4 address in the system!");
	}

	public List<IPSegment> GetInterfaces(bool showVPN) {
		List<IPSegment> ipsList = new List<IPSegment>();
		foreach(NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces()) {
			if(ni.Name.Contains("VM") || ni.Name.Contains("Loopback"))
				continue;
			if(!showVPN && ni.Name.Contains("VPN"))
				continue;
			foreach(UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses) {
				if(ip.Address.AddressFamily == AddressFamily.InterNetwork) {
					int subnet = Regex.Matches(
						Convert.ToString(IPHelper.ParseIp(ip.IPv4Mask.ToString()), 2).ToString(), "1").Count;
					if(subnet < maxSubnet)
						continue;
					IPSegment ips = new IPSegment(ip.Address.ToString(), ip.IPv4Mask.ToString());
					ipsList.Add(ips);
				}
			}
		}
		return ipsList;
	}

	int instances = 0;
	int instancesInfo = 0;
	public void RequestGames(List<string> addresses) {
		StopAllCoroutines();
		state = LobbyCallState.LOADING;
		instances = addresses.Count;
		instancesInfo = 0;
		foreach(string address in addresses) {
			StartCoroutine(RequestGamesAsync(address));
		}
	}

	public IEnumerator RequestGamesAsync(string address) {
		List<string> games = new List<string>();
		UnityWebRequest request = Http.CreateRequest(address, "games", null);
		yield return request.SendWebRequest();
		instances--;
		int[] gameIds = JsonConvert.DeserializeObject<int[]>(request.downloadHandler.text);
		if(gameIds == null) {

		} else {
			instancesInfo += gameIds.Length;
			for(int i = 0; i < gameIds.Length; i++) {
				StartCoroutine(RequestGameInfo(address, gameIds[i]));
			}
		}
		float ttl = 5f;
		float time = 0;
		while(instancesInfo > 0) {
			yield return new WaitForSeconds(0.05f);
			time += 0.05f;
			if(time >= ttl) {
				instances = 0;
				break;
			}
		}
		if(state == LobbyCallState.LOADING) {
			if(this.games.Count == 0) {
				state = LobbyCallState.NO_GAMES_FOUND;
			} else {
				state = LobbyCallState.LOADED;
			}
		}
	}

	public IEnumerator RequestGameInfo(string address, int gameId) {
		UnityWebRequest request = Http.CreateRequest(address, "games/" + gameId + "/status", null);
		yield return request.SendWebRequest();
		if(request.responseCode == 200) {
			(GameInfo, string, int) t = (
				JsonConvert.DeserializeObject<GameInfo>(request.downloadHandler.text), 
				address,
				gameId
			);
			this.games.Add(t);
		}
		instancesInfo--;
	}

	public void JoinLobby(string address, int gameId, string password, string playerName) {
		StartCoroutine(JoinLobbyAsync(address, gameId, password, playerName));
	}

	public IEnumerator JoinLobbyAsync(string address, int gameId, string password, string playerName) {
		if(password != null) {
			password = "password=" + password;
		}
		playerName = "name=" + playerName;
		UnityWebRequest request = Http.CreatePost(
			address, 
			"games/" + gameId + "/players", 
			password, playerName
		);
		yield return request.SendWebRequest();
		if(request.downloadHandler != null)
			Debug.Log(request.downloadHandler.text);
	}
}
