using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;


namespace RoboRally.Utils {
	public class IngameData {

		public static JoinResponse JoinData = null;
		
		public static List<string> PlayerNames = new List<string>();
		public static string PlayerName {
			set {
				PlayerNames.Remove(PlayerName);
				PlayerNames.Add(value);
				PlayerName = value;
			}
			get { return PlayerName; }
		}

		public static bool IsHost = false;

		public static int ID = -1;
	}
}
