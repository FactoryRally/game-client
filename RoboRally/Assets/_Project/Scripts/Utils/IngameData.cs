using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;


namespace RoboRally.Utils {
	public class IngameData {

		public static JoinResponse JoinData = null;

		public static Map SelectedMap = new Map();
		
		public static List<string> PlayerNames = new List<string>();

		private static string _playerName = "";
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

		public static int GameId = -1;
		public static int PlayerId = -1;

		public static string Address = "";
		public static int    MyRobotId = -1;

		public static void Reset() {
			IngameData.Address = "localhost";
			IngameData.GameId = -1;
			IngameData.PlayerId = -1;
			IngameData.IsHost = true;
			IngameData.JoinData = null;
			IngameData.MyRobotId = -1;
			IngameData.PlayerNames = new List<string>();
			IngameData.SelectedMap = new Map();
		}
	}
}
