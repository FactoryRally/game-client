using System.Net.NetworkInformation;
using System.Threading;
using System.Collections.Generic;
using System;
using UnityEngine;
using Ping = System.Net.NetworkInformation.Ping;
using System.Threading.Tasks;

public class AddressFinder {

	public List<string> addresses = new List<string>();
	private MonoBehaviour m;

	Thread myThread = null;
	public bool finished = false;
	public int instances = 0;

	public AddressFinder(MonoBehaviour mono) {
		m = mono;
	}

	public void Scan(List<IPSegment> iPSegments, Action<List<string>> addresses) {
		myThread = new Thread(() => ScanThreads(iPSegments, addresses));
		myThread.Start();
	}

	private void ScanThreads(List<IPSegment> iPSegments, Action<List<string>> callback) {
		Ping myPing;
		PingReply reply;
		instances = 0;

		foreach(IPSegment ips in iPSegments) {
			foreach(uint hosta in ips.Hosts()) {
				string ip = IPHelper.ToIpString(hosta);
				Task.Factory.StartNew(() => {
					myPing = new Ping();
					instances++;
					reply = myPing.Send(ip, 250);

					if(reply.Status == IPStatus.Success) {
						addresses.Add(ip);
					}
					instances--;
				}, TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning);
			}
		}
		int i = 0;
		while(instances > 0) {
			if(i > 100)
				break;
			i++;
			Thread.Sleep(10);
		}
		callback(addresses);
		finished = true;
	}
}