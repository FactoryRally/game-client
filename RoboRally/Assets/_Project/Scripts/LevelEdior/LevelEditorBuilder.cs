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

	[MyBox.Separator("Coloring")]
	public Material DefaultMaterial;
	public Material HoverMaterial; // Gets shown if it is possible to place
	public Material RestrictedMaterial; // Gets shown if it is not possible to place

	[MyBox.Separator("Cursor")]
	public Texture2D HandOpenCursor;
	public Texture2D HandClosedCursor;

	private Vector3 GridPos;
	private GameObject CurrentTile;
	protected Quaternion TileRotation = Quaternion.identity;
	protected Direction RotaterDirection = Direction.Left;
	protected int TileLevel = 0;

	void Start() {
		CurrentMap = new Map();
	}


	void Update() {
		if(LevelGrid.IsFocused)
			UpdateGridPos();

		UpdateHoverEffect();

		if(Input.GetMouseButtonDown(0) && CurrentTile != null) {
			TileType type = CurrentTile.GetComponent<TileObject>().TileType;
			Direction direction = QuaternionToDirection(TileRotation);
			CurrentMap[(int) (GridPos.z / LevelGrid.TileUnitSize), (int) (GridPos.x / LevelGrid.TileUnitSize)] = new Tile(
				type, 
				true,
				direction,
				RotaterDirection
			);
			UpdateMap();
		}
	}

	private void UpdateMap() {
		for(int i = 0; i < GridChilds.transform.childCount; i++) {
			Destroy(GridChilds.transform.GetChild(i).gameObject);
		}
		for(int c = 0; c < CurrentMap.columnCount; c++) {
			for(int r = 0; r < CurrentMap.rowCount; r++) {
				for(int i = 0; i < Tiles.Count; i++) {
					if(Tiles[i].GetComponent<TileObject>().TileType == CurrentMap[c, r].Type) {
						GameObject gj = Instantiate(
							Tiles[i], 
							new Vector3(r, TileLevel + 1, c), 
							DirectionToQuaternion(CurrentMap[c, r].TileDirection)
						);
						gj.transform.parent = GridChilds.transform;
					}
				}
			}
		}
	}

	private void UpdateGridPos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)) {
			GridPos = new Vector3(
					Mathf.Round(hit.point.x / LevelGrid.TileUnitSize) * LevelGrid.TileUnitSize,
					Mathf.Round(hit.point.y / LevelGrid.TileUnitSize) * LevelGrid.TileUnitSize + 1,
					Mathf.Round(hit.point.z / LevelGrid.TileUnitSize) * LevelGrid.TileUnitSize
				);
		}
	}

	private void UpdateHoverEffect() {
		EventSystem eve = EventSystem.current;

		if(eve.IsPointerOverGameObject()) { // Is GUI focused
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			if(CurrentTile != null) {
				Destroy(CurrentTile);
				CurrentTile = null;
			}
		} else if(CurrentTileIndex == -1) {  // If no Tile is selected
			if(CurrentTile != null) {
				Destroy(CurrentTile);
				CurrentTile = null;
			}
			if(Input.GetMouseButton(0)) {
				Cursor.SetCursor(HandClosedCursor, Vector2.zero, CursorMode.Auto);
			} else {
				Cursor.SetCursor(HandOpenCursor, Vector2.zero, CursorMode.Auto);
			}
		} else { // If a tile is selected
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			if(CurrentTile != null)
				Destroy(CurrentTile);
			if(eve.IsPointerOverGameObject()) // If GUI is focused 
				return;
			CurrentTile = Instantiate(Tiles[CurrentTileIndex], GridPos, TileRotation);
		}
	}

	public void SelectTile(int index) {
		CurrentTileIndex = index;
	}

	public void RotateTile(bool left) {
		if(left) {
			TileRotation *= Quaternion.Euler(0, -90, 0);
		} else {
			TileRotation *= Quaternion.Euler(0, 90, 0);
		}
	}

	public Direction QuaternionToDirection(Quaternion quaternion) {
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

	public Quaternion DirectionToQuaternion(Direction direction) {
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