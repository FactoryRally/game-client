using Gameclient.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditorBuilder : MonoBehaviour {

	[MyBox.Separator("General")]
	public Grid LevelGrid;
	public GameObject GridChilds;
	public Map CurrentMap;
	public LevelEditorController lec;

	[MyBox.Separator("Tiles")]
	public List<GameObject> Tiles;
	public int CurrentTileIndex;

	[MyBox.Separator("Map")]
	public int minSize = 10;
	public int maxSize = 50;

	[MyBox.Separator("Coloring")]
	public Material DefaultMaterial;
	public Material HoverMaterial; // Gets shown if it is possible to place
	public Material RestrictedMaterial; // Gets shown if it is not possible to place

	private Vector3 GridPos;
	private Vector2 MapPos;
	private GameObject CurrentTile;
	private GameObject[,] GridObjects;

	[MyBox.ReadOnly]
	public Quaternion TileRotation = Quaternion.identity;
	[MyBox.ReadOnly]
	public Direction RotaterDirection = Direction.Left;
	[MyBox.ReadOnly]
	public int TileLevel = 0;

	[MyBox.ReadOnly]
	public bool MoveSelected;
	[MyBox.ReadOnly]
	public bool MouseOverGUI;
	private bool MouseOverGUIHolded;


	void Awake() {
		lec = GetComponent<LevelEditorController>();
	}

	void Start() {
		CurrentMap = new Map();
		GridObjects = new GameObject[CurrentMap.Width, CurrentMap.Height];
		UpdateMap();
	}


	void Update() {
		UpdateGridPos();
		UpdateMouseState();
		UpdateHoverEffect();

		LevelGrid.transform.localScale = new Vector3(CurrentMap.Width, 1, CurrentMap.Height);

		if(Input.GetMouseButton(0) && CurrentTile != null) {
			TileType type = CurrentTile.GetComponent<TileObject>().TileType;
			Direction direction = QuaternionToDirection(TileRotation);
			CurrentMap[(int) MapPos.x, (int) MapPos.y] = new Tile(
					type,
					true,
					direction,
					RotaterDirection,
					TileLevel
			);
			UpdateMap();
		}
		if(Input.GetMouseButton(1) && CurrentTile != null) {
			CurrentMap[(int) MapPos.x, (int) MapPos.y] = null;
			UpdateMap();
		}
	}

	public void UpdateMap() {
		if(GridObjects.GetLength(0) == CurrentMap.Width && GridObjects.GetLength(1) == CurrentMap.Height) {
			for(int c = 0; c < CurrentMap.Width; c++) {
				for(int r = 0; r < CurrentMap.Height; r++) {
					if(CurrentMap[c, r] == null) {
						if(GridObjects[c, r] != null)
							Destroy(GridObjects[c, r]);
						GridObjects[c, r] = null;
						continue;
					}
					if(GridObjects[c, r] == null ||
							GridObjects[c, r].GetComponent<TileObject>().TileType != CurrentMap[c, r].Type) {
						GameObject tile = GetTileFromType(CurrentMap[c, r].Type);
						Destroy(GridObjects[c, r]);
						GridObjects[c, r] = Instantiate(
							tile,
							new Vector3(c, CurrentMap[c, r].Level + 1, CurrentMap.Height - r - 1),
							DirectionToQuaternion(CurrentMap[c, r].TileDirection)
						);
						GridObjects[c, r].transform.parent = GridChilds.transform;
					}
				}
			}
		} else {
			for(int c = 0; c < GridObjects.GetLength(0); c++) {
				for(int r = 0; r < GridObjects.GetLength(1); r++) {
					if(GridObjects[c, r] != null)
						Destroy(GridObjects[c, r]);
				}
			}
			GridObjects = new GameObject[CurrentMap.Width, CurrentMap.Height];
			for(int c = 0; c < CurrentMap.Width; c++) {
				for(int r = 0; r < CurrentMap.Height; r++) {
					if(CurrentMap[c, r] == null)
						continue;
					GameObject tile = GetTileFromType(CurrentMap[c, r].Type);
					if(tile == null)
						continue;
					GridObjects[c, r] = Instantiate(
						tile,
						new Vector3(c, CurrentMap[c, r].Level + 1, CurrentMap.Height - r - 1),
						DirectionToQuaternion(CurrentMap[c, r].TileDirection)
					);
					GridObjects[c, r].transform.parent = GridChilds.transform;
				}
			}
		}
	}

	public GameObject GetTileFromType(TileType type) {
		for(int i = 0; i < Tiles.Count; i++) {
			if(Tiles[i] == null)
				continue;
			if(Tiles[i].GetComponent<TileObject>().TileType == type) {
				return Tiles[i];
			}
		}
		return null;
	}

	private void UpdateGridPos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// , Mathf.Infinity, 1 << 9
		if(Physics.Raycast(ray, out hit)) {
			GridPos = new Vector3(
					Mathf.Round(hit.point.x / LevelGrid.TileUnitSize) * LevelGrid.TileUnitSize,
					1,
					Mathf.Round(hit.point.z / LevelGrid.TileUnitSize) * LevelGrid.TileUnitSize
				);
			MapPos = new Vector2(
					GridPos.x,
					(CurrentMap.Height - 1) - GridPos.z
				);
		}
	}

	private void UpdateMouseState() {
		EventSystem eve = EventSystem.current;
		if(Input.GetMouseButtonDown(0) && eve.IsPointerOverGameObject() || MouseOverGUIHolded)
			MouseOverGUIHolded = true;
		if(Input.GetMouseButtonUp(0))
			MouseOverGUIHolded = false;
		MouseOverGUI = eve.IsPointerOverGameObject() || MouseOverGUIHolded;
		MoveSelected = false;
	}

	private void UpdateHoverEffect() {
		if(MouseOverGUI) { // Is GUI focused
			lec.MainCamera.DefaultCursor = true;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			
			if(CurrentTile != null) {
				Destroy(CurrentTile);
				CurrentTile = null;
			}
		} else if(CurrentTileIndex == -1) {  // If no Tile is selected
			lec.MainCamera.DefaultCursor = false;
			MoveSelected = true;
			if(CurrentTile != null) {
				Destroy(CurrentTile);
				CurrentTile = null;
			}
		} else { // If a tile is selected
			lec.MainCamera.DefaultCursor = true;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			if(CurrentTile != null)
				Destroy(CurrentTile);
			if(MouseOverGUI) // If GUI is focused 
				return;

			GridPos = new Vector3(
					GridPos.x,
					TileLevel + 1,
					GridPos.z
				);
			CurrentTile = Instantiate(Tiles[CurrentTileIndex], GridPos, TileRotation);
		}
	}

	public void SelectTile(int index) {
		if(index == -1 || (index < Tiles.Count && Tiles[index] != null)) {
			CurrentTileIndex = index;
		}
	}

	public void RotateTile(bool left) {
		if(left) {
			TileRotation *= Quaternion.Euler(0, -90, 0);
		} else {
			TileRotation *= Quaternion.Euler(0, 90, 0);
		}
	}

	public void Save() {
		if(CurrentMap.IsValid()) {

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
}