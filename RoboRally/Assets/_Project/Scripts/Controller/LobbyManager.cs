using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Networking;
using Ping = System.Net.NetworkInformation.Ping;

public class LobbyManager : MonoBehaviour {

	public UnityEngine.Object[] Games;
	private List<string> localAddresses = new List<string>();
	private List<string> hostAddresses = new List<string>();
	private bool load = true;
	private int maxSubnet = 24;
	AddressFinder af;

	public void Start() {
		List<IPSegment> iPSegments = GetInterfaces(false);
		af = new AddressFinder(this);
		af.Scan(
			iPSegments,
			(addresses) => {
				localAddresses = addresses;
			}
		);
	}

	public void Update() {
		if(af.finished && load) {
			load = false;
			foreach(string address in localAddresses) {

			}
		}
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

	public void RequestGames(Action<string[]> callback) {
		StartCoroutine(RequestGamesAsync(callback));
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
