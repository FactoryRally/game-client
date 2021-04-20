using Newtonsoft.Json;
using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Objects {
	public class MapBuilder : MonoBehaviour {

		private static MapBuilder _instance;
		public static MapBuilder Instance { get { return _instance; } }

		public Map SelectedMap = new Map(10, 10);

		public int WallDistanceModifier = 5;

		public GameObject Floor;
		public GameObject SpaceFiller;
		public GameObject WallPrefab;
		public List<GameObject> TilePrefabs = new List<GameObject>();
		public List<GameObject> Tiles = new List<GameObject>();
		private List<GameObject> Walls = new List<GameObject>();


		void Awake() {
			if(_instance != null && _instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				_instance = this;
			}
		}

		void Start() {

		}


		void Update() {

		}


		public GameObject PrefabByType(TileType type, int order) {
			for(int i = 0; i < TilePrefabs.Count; i++) {
				if(TilePrefabs[i] == null || TilePrefabs[i].GetComponent<TileObject>() == null)
					continue;
				if(TilePrefabs[i].GetComponent<TileObject>().TileType == type) {
					if(type == TileType.Checkpoint && order != -1) { // for Checkpoints check Order first
						if(TilePrefabs[i].name.EndsWith("_" + order)) {
							return TilePrefabs[i];
						}
						continue;
					}
					return TilePrefabs[i];
				}
			}
			return null;
		}

		public void BuildMap(bool clear = true, bool buildWalls = true) {
			if(clear)
				ClearMap();

			for(int x = 0; x < SelectedMap.Width; x++) {
				for(int y = 0; y < SelectedMap.Height; y++) {
					Tile tile = SelectedMap[x, y];
					GameObject tilePrefab = PrefabByType(tile.Type, tile.Order);
					if(tilePrefab == null) {
						this.Tiles.Add(null);
						continue;
					}
					GameObject tileObject = Instantiate(
						tilePrefab,
						new Vector3(x, tile.Level, y),
						DirectionToQuaternion((Direction) tile.Direction),
						transform
					);
					tileObject.name = "[" + x + ", " + y + "]" + tile.Type;
					this.Tiles.Add(tileObject);
				}
			}

			if(buildWalls)
				SetupWalls();
		}

		public void ClearMap() {
			foreach(GameObject tile in this.Tiles) {
				Destroy(tile);
			}
			this.Tiles.Clear();
			foreach(GameObject wall in this.Walls) {
				Destroy(wall);
			}
			this.Walls.Clear();
		}

		private void SetupWalls() {
			Floor.transform.localScale = new Vector3(
				SelectedMap.Width + 2 * WallDistanceModifier,
				1,
				SelectedMap.Height + 2 * WallDistanceModifier
			);
			Floor.transform.position = GetCenter() + 2 * Vector3.down;
			SpaceFiller.transform.localScale = new Vector3(
				SelectedMap.Width,
				2,
				SelectedMap.Height
			);
			SpaceFiller.transform.position = GetCenter() + 0.5f * Vector3.down;
			for(int i = 0; i < 4; i++) {
				GameObject wall = Instantiate(
					WallPrefab,
					GetSide(i) + 4 * Vector3.up,
					Quaternion.Euler(0, i * 90, 0)
				);
				wall.name = "Wall_" + i;
				int width = i % 2 == 0 ? SelectedMap.Height : SelectedMap.Width;
				wall.GetComponent<Wall>().SetWidth(width + 2 * WallDistanceModifier);
				this.Walls.Add(wall);
			}
		}

		public Vector3 GetSide(int side) {
			switch(side) {
				case 0:
					return GetCenter() + new Vector3((-SelectedMap.Width - 1) / 2f - WallDistanceModifier, 0, 0);
				case 1:
					return GetCenter() + new Vector3(0, 0, (SelectedMap.Height + 1) / 2f + WallDistanceModifier);
				case 2:
					return GetCenter() + new Vector3((SelectedMap.Width + 1) / 2f + WallDistanceModifier, 0, 0);
				case 3:
					return GetCenter() + new Vector3(0, 0, (-SelectedMap.Height - 1) / 2f - WallDistanceModifier);
			}
			return GetCenter() + new Vector3(-SelectedMap.Width / 2f - 1, 0, 0);
		}

		public Vector3 GetCenter() {
			return new Vector3((SelectedMap.Width - 1) / 2f, 0, (SelectedMap.Height - 1) / 2f);
		}

		public static Quaternion DirectionToQuaternion(Direction direction) {
			switch(direction) {
				case Direction.Up:
					return Quaternion.Euler(0, 0, 0);
				case Direction.Right:
					return Quaternion.Euler(0, 90, 0);
				case Direction.Down:
					return Quaternion.Euler(0, 180, 0);
				case Direction.Left:
					return Quaternion.Euler(0, 270, 0);
				default:
					return Quaternion.Euler(0, 0, 0);
			}
		}

		public static Direction QuaternionToDirection(Quaternion quaternion) {
			float y = quaternion.eulerAngles.y;
			switch(y) {
				case 0:
					return Direction.Up;
				case 90:
					return Direction.Right;
				case 180:
					return Direction.Down;
				case 270:
					return Direction.Left;
				default:
					Debug.Log(y);
					return Direction.Up;
			}
		}
	}
}
