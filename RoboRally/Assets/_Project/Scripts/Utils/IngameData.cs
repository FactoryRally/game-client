using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;


namespace RoboRally.Utils {
	public class IngameData {

		public static JoinResponse JoinData = null;

		public static Map SelectedMap = new Map();
		
		public static List<string> PlayerNames = new List<string>();

		public static string _playerName = "";
		public static string PlayerName {
			get { return _playerName; }
			set {
				if(PlayerNames.Contains(_playerName))
					PlayerNames.Remove(_playerName);
				PlayerNames.Add(value);
				_playerName = value;
			}
		}

		public static bool IsHost = true;

		public static int ID = -1;

		public static string Address = "";
	}
}
