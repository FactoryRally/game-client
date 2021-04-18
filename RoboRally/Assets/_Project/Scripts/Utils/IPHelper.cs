using System;
using System.Globalization;

namespace RoboRally.Utils {
	public static class IPHelper {

		public static string ToIpString(uint value) {
			uint bitmask = 0xff000000;
			string[] parts = new string[4];
			for(var i = 0; i < 4; i++) {
				uint masked = (value & bitmask) >> ((3 - i) * 8);
				bitmask >>= 8;
				parts[i] = masked.ToString(CultureInfo.InvariantCulture);
			}
			return String.Join(".", parts);
		}

		public static uint ParseIp(string ipAddress) {
			string[] splitted = ipAddress.Split('.');
			uint ip = 0;
			for(var i = 0; i < 4; i++) {
				ip = (ip << 8) + uint.Parse(splitted[i]);
			}
			Console.WriteLine(~ip);
			return ip;
		}
	}
}