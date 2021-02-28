using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : MonoBehaviour {

	public List<string> localAddresses = new List<string>();
	public List<string> hostAddresses = new List<string>();
	public List<(GameInfo game, int id)> games = new List<(GameInfo game, int id)>();

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

	public void GetGames(bool reloadHosts = false) {
		if(state == LobbyCallState.LOADING)
			return;
		this.games = new List<(GameInfo game, int id)>();
		if(reloadHosts) {
			GetLocalAddresses();
		} else {
			state = LobbyCallState.SCANNED;
		}
	}

	public void GetLocalAddresses() {
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
	public void RequestGames(List<string> addresses) {
		state = LobbyCallState.LOADING;
		instances = addresses.Count;
		foreach(string address in addresses) {
			StartCoroutine(RequestGamesAsync(address));
		}
	}

	public IEnumerator RequestGamesAsync(string address) {
		List<string> games = new List<string>();
		UnityWebRequest request = Http.CreateRequest(address, "games", null);
		yield return request.SendWebRequest();
		int[] gameIds = JsonConvert.DeserializeObject<int[]>(request.downloadHandler.text);
		if(gameIds == null) {

		} else {
			for(int i = 0; i < gameIds.Length; i++) {
				request = Http.CreateRequest(address, "games/" + gameIds[i] + "/status", null);
				yield return request.SendWebRequest();
				if(request.responseCode == 200) {
					(GameInfo, int) t = (JsonConvert.DeserializeObject<GameInfo>(request.downloadHandler.text), gameIds[i]);
					this.games.Add(t);
				}

			}
		}
		instances--;
		if(state == LobbyCallState.LOADING && instances == 0) {
			if(this.games.Count == 0) {
				state = LobbyCallState.NO_GAMES_FOUND;
			} else {
				state = LobbyCallState.LOADED;
			}
		}
	}

	public void JoinLobby(int gameID) {

	}
}
