using Newtonsoft.Json;
using RoboRally.Objects;
using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Controller {
	public partial class GameController : MonoBehaviour {

		private static GameController _instance;
		public static GameController Instance { get { return _instance; } }


		[Serializable]
		public struct RobotPrefab {
			public Robots RobotType;
			public GameObject Prefab;
		}
		public List<RobotPrefab> RobotPrefs = new List<RobotPrefab>();
		public Dictionary<Robots, GameObject> roboPrefabs => RobotPrefs.ToDictionary(t => t.RobotType, t => t.Prefab);
		private Dictionary<int, GameObject> IngameRobots = new Dictionary<int, GameObject>();

		void Start() {
			if(_instance != null && _instance != this) {
				Destroy(gameObject);
			} else {
				_instance = this;
			}
		}

		public void SpawnRobot(RobotInfo robotInfo) {
			if(robotInfo.Type == 0)
				robotInfo.Type = (Robots) 1;
			GameObject robotPrefab = roboPrefabs[robotInfo.Type];
			Debug.Log("Robot Level" + IngameData.SelectedMap[robotInfo.Location.X, robotInfo.Location.Y].Level);
			GameObject robot = Instantiate(
				robotPrefab,
				new Vector3(
					robotInfo.Location.X,
					IngameData.SelectedMap[robotInfo.Location.X, robotInfo.Location.Y].Level + 1,
					robotInfo.Location.Y
				),
				MapBuilder.DirectionToQuaternion(robotInfo.Direction)
			);
			IngameRobots.Add(robotInfo.Id, robot);
		}

		

		


		public void GetRobots(string address, int gameId, System.Action<int[]> action = null) {
			StartCoroutine(GetRobotsAsync(address, gameId, action));
		}

		public IEnumerator GetRobotsAsync(string address, int gameId, System.Action<int[]> action) {
			UnityWebRequest request = Http.CreateGet(
				"games/" + gameId + "/entities/robots",
				Http.AuthOnlyParams
			);
			return Http.SendWithCallback(request, action);
		}

		public void GetRobotInfo(string address, int gameId, int robotId, System.Action<RobotInfo> action = null) {
			StartCoroutine(GetRobotInfoAsync(gameId, robotId, action));
		}

		public IEnumerator GetRobotInfoAsync(int gameId, int robotId, Action<RobotInfo> action) {
			UnityWebRequest request = Http.CreateGet(
				"games/" + gameId + "/entities/robots/" + robotId + "/info",
				Http.AuthOnlyParams
			);
			return Http.SendWithCallback(request, action);
		}

		public void GetMap(int gameId, System.Action<Map> action) {
			if(IngameData.JoinData == null)
				return;
			StartCoroutine(GetMapAsync(gameId, action));
		}

		public IEnumerator GetMapAsync(int gameId, Action<Map> action) {
			UnityWebRequest request = Http.CreateGet(
				"games/" + gameId + "/map",
				Http.AuthOnlyParams
			);
			return Http.SendWithCallback(request, action);
		}


		public void HandleMoveEvent(MovementEvent moveEvent) {

		}

		public void OnPickRobot(RobotPickEvent ev) {
			if (ev.Player.Equals(IngameData.JoinData.Id))
				IngameData.MyRobotId = ev.Robot;
		}

		public void HandleMapCreateEvent() {
			GetMap(IngameData.GameId, map => {
				IngameData.SelectedMap          = map;
				MapBuilder.Instance.SelectedMap = map;
				MapBuilder.Instance.BuildMap();
				GetRobots(
					IngameData.Address, 
					IngameData.GameId,
					SpawnRobots
				);
			});
		}

		public void SpawnRobots(int[] robotIds) {
			foreach(int robotId in robotIds) {
				GetRobotInfo(
					IngameData.Address,
					IngameData.GameId,
					robotId,
					SpawnRobot
				);
			}
		}
	}
}