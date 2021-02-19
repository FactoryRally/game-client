using System.Net.NetworkInformation;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Net;
using UnityEngine;
using Ping = System.Net.NetworkInformation.Ping;
using System.Collections;

public class AddressFinderOld {

	private List<Ping> pingers = new List<Ping>();
	private List<IPAddress> addresses = new List<IPAddress>();

	private int timeOut = 250;
	private int ttl = 5;
	private int instances;
	private MonoBehaviour m;

	public void Scan(MonoBehaviour mono, IPSegment ips, Action<List<IPAddress>> callback) {
		this.m = mono;
		m.StartCoroutine(ScanAsync(ips, callback));
	}

	private IEnumerator ScanAsync(IPSegment ips, Action<List<IPAddress>> callback) {
		PingOptions po = new PingOptions(ttl, true);
		byte[] data = new System.Text.ASCIIEncoding().GetBytes("abababababababababababababababab");
		instances = 0;
		Debug.Log(ips.NumberOfHosts);
		foreach(uint host in ips.Hosts()) {
			m.StartCoroutine(Send(IPHelper.ToIpString(host), data, po));
			yield return new WaitForSeconds(0.01f);
		}
		Debug.Log(instances);
		WaitForSeconds wait = new WaitForSeconds(0.05f);
		while(instances > 0) {
			yield return wait;
		}
		callback(addresses);
	}

	private IEnumerator Send(string ip, byte[] data, PingOptions po) {
		instances++;
		Ping p = new Ping();
		PingReply rep = p.SendPingAsync(ip, timeOut, data, po).Result;
		p.Dispose();
		if(rep.Status == IPStatus.Success)
			addresses.Add(IPAddress.Parse(ip));
		instances--;
		yield return new WaitForSeconds(0);
	}
}