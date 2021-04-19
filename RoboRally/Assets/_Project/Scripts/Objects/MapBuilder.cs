using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.Objects {
	public class MapBuilder : MonoBehaviour {

		private static MapBuilder _instance;
		public static MapBuilder Instance { get { return _instance; } }

		public Map SelectedMap = new Map();

		public GameObject GameGrid;
		public List<GameObject> TilePrefabs = new List<GameObject>();
		public List<GameObject> Tiles = new List<GameObject>();


		void Start() {
			if(_instance != null && _instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				_instance = this;
			}
		}


		void Update() {

		}


		public GameObject PrefabByType(TileType type) {
			for(int i = 0; i < Tiles.Count; i++) {
				if(Tiles[i] == null || Tiles[i].GetComponent<TileObject>())
					continue;
				if(Tiles[i].GetComponent<TileObject>().TileType == type) {
					return Tiles[i];
				}
			}
			return null;
		}

		public void BuildMap(bool clear = true) {
			if(clear)
				ClearMap();
			for(int x = 0; x < SelectedMap.Width; x++) {
				for(int y = 0; y < SelectedMap.Height; y++) {
					Tile tile = SelectedMap[x, y];
					GameObject tilePrefab = PrefabByType(tile.Type);
					GameObject tileObject = Instantiate(
						tilePrefab,
						new Vector3(x, y),
						DirectionToQuaternion(tile.TileDirection),
						transform
					);
					tileObject.name = "[" + x + ", " + y + "]" + tile.Type;
					this.Tiles.Add(tileObject);
				}
			}
		}

		public void ClearMap() {
			foreach(GameObject tile in this.Tiles) {
				Destroy(tile);
			}
			this.Tiles.Clear();
		}

		public Vector3 GetCenter() {
			return new Vector3(SelectedMap.Width / 2f, 0, SelectedMap.Height / 2f);
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
