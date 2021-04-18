using System.Collections.Generic;
using System;

namespace RoboRally.Utils {
	public class IPSegment {

		private uint _ip;
		private uint _mask;

		public IPSegment(string ip, string mask) {
			_ip = IPHelper.ParseIp(ip);
			_mask = IPHelper.ParseIp(mask);
		}

		public uint NumberOfHosts {
			get { return ~_mask + 1; }
		}

		public uint NetworkAddress {
			get { return _ip & _mask; }
		}

		public uint BroadcastAddress {
			get { return NetworkAddress + ~_mask; }
		}

		public IEnumerable<uint> Hosts() {
			for(uint host = NetworkAddress + 1; host < BroadcastAddress; host++) {
				yield return host;
			}
		}
	}
}