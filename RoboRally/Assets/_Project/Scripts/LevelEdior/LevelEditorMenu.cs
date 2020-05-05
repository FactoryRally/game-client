using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelEditorMenu : MonoBehaviour {

	public LevelEditorBuilder leb;

	[MyBox.Separator("Tile Config")]
	public TMP_InputField LevelInput;
	public TMP_Dropdown DirectionDropdown;
	public TMP_Dropdown RotationDropdown;
	public int ShowRotationOnTileIndex = 5;

	private int LastTileLevel = 0;
	private Direction LastDirectionValue = Direction.Up;
	private Direction LastRotaterDirectionValue = Direction.Left;

	[MyBox.Separator("Map Config")]
	public TMP_InputField ColumnCountInput;
	public TMP_InputField RowCountInput;
	private int LastColumnCount;
	private int LastRowCount;

	public TMP_InputField AddColumnAtInput;
	public Slider AddColumnAtSlider;

	public TMP_InputField AddRowAtInput;
	public Slider AddRowAtSlider;

	public TMP_InputField RemoveColumnAtInput;
	public Slider RemoveColumnAtSlider;

	public TMP_InputField RemoveRowAtInput;
	public Slider RemoveRowAtSlider;



	void Awake() {
		leb = GetComponent<LevelEditorBuilder>();
	}

	void Start() {

	}

	void OnGUI() {
		// Level validation
		try {
			int level = int.Parse(LevelInput.text);
			if(level > 1) {
				LevelInput.text = "1";
			}
			if(level < 0) {
				LevelInput.text = "0";
			}
		} catch(FormatException e) {

		}

		// Show RotationDropdown if needed
		RotationDropdown.transform.parent.gameObject.SetActive(leb.CurrentTileIndex == ShowRotationOnTileIndex);

		// Set Slider Min and Max values
		AddColumnAtSlider.maxValue = leb.CurrentMap.columnCount;
		AddRowAtSlider.maxValue = leb.CurrentMap.rowCount;

		RemoveColumnAtSlider.maxValue = leb.CurrentMap.columnCount - 1;
		RemoveRowAtSlider.maxValue = leb.CurrentMap.rowCount - 1;

		// Map size validation
		try {
			int columnCount = int.Parse(ColumnCountInput.text);
			if(columnCount > leb.maxSize) {
				ColumnCountInput.text = leb.maxSize + "";
			}
			if(columnCount < leb.minSize) {
				ColumnCountInput.text = leb.minSize + "";
			}
		} catch(FormatException e) {

		}

		try {
			int rowCount = int.Parse(RowCountInput.text);
			if(rowCount > leb.maxSize) {
				RowCountInput.text = leb.maxSize + "";
			}
			if(rowCount < leb.minSize) {
				RowCountInput.text = leb.minSize + "";
			}
		} catch(FormatException e) {

		}
	}

	void Update() {
		// Set Level
		if(LastTileLevel.ToString() == LevelInput.text) {
			LevelInput.text = leb.TileLevel.ToString();
		} else {
			try {
				int level = int.Parse(LevelInput.text);
				leb.TileLevel = level;
			} catch(FormatException e) {

			}
		}
		LastTileLevel = leb.TileLevel;

		// Set Direction
		if((int) LastDirectionValue == DirectionDropdown.value) {
			DirectionDropdown.value = (int) LevelEditorBuilder.QuaternionToDirection(leb.TileRotation);
		} else {
			leb.TileRotation = LevelEditorBuilder.DirectionToQuaternion((Direction) DirectionDropdown.value);
		}
		LastDirectionValue = LevelEditorBuilder.QuaternionToDirection(leb.TileRotation);

		// Set Rotater rotation
		if((int) LastRotaterDirectionValue == RotationDropdown.value) {
			RotationDropdown.value = (int) leb.RotaterDirection;
		} else {
			leb.RotaterDirection = (Direction) RotationDropdown.value;
		}
		LastRotaterDirectionValue = leb.RotaterDirection;
	}

	public void AddColumn() {
		leb.CurrentMap.AddColumn((int) AddColumnAtSlider.value);
		leb.UpdateMap();
		UpdateMapSize();
	}

	public void AddRow() {
		leb.CurrentMap.AddRow((int) AddRowAtSlider.value);
		leb.UpdateMap();
		UpdateMapSize();
	}

	public void RemoveColumn() {
		leb.CurrentMap.RemoveColumn((int) RemoveColumnAtSlider.value);
		leb.UpdateMap();
		UpdateMapSize();
	}

	public void RemoveRow() {
		leb.CurrentMap.RemoveRow((int) RemoveColumnAtSlider.value);
		leb.UpdateMap();
		UpdateMapSize();
	}

	public void UpdateMapSize() {
		if(LastColumnCount.ToString() == ColumnCountInput.text) {
			ColumnCountInput.text = leb.CurrentMap.columnCount.ToString();
		} else {
			try {
				int columnCount = int.Parse(ColumnCountInput.text);
				while(columnCount > leb.CurrentMap.columnCount) {
					leb.CurrentMap.AddColumn(leb.CurrentMap.columnCount);
				}
				while(columnCount < leb.CurrentMap.columnCount) {
					leb.CurrentMap.RemoveColumn(leb.CurrentMap.columnCount - 1);
				}
				leb.UpdateMap();
			} catch(FormatException e) {

			}
		}
		LastColumnCount = leb.CurrentMap.columnCount;

		if(LastRowCount.ToString() == RowCountInput.text) {
			RowCountInput.text = leb.CurrentMap.rowCount.ToString();
		} else {
			try {
				int rowCount = int.Parse(RowCountInput.text);
				while(rowCount > leb.CurrentMap.rowCount) {
					leb.CurrentMap.AddRow(leb.CurrentMap.rowCount);
				}
				while(rowCount < leb.CurrentMap.rowCount) {
					leb.CurrentMap.RemoveRow(leb.CurrentMap.rowCount - 1);
				}
				leb.UpdateMap();
			} catch(FormatException e) {

			}
		}
		LastRowCount = leb.CurrentMap.rowCount;
	}
}
