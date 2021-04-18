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
	public TMP_InputField WidthInput;
	public TMP_InputField HeightInput;
	private int LastWidth;
	private int LastHeight;

	public TMP_InputField AddColumnAtInput;
	public Slider AddColumnAtSlider;

	public TMP_InputField AddRowAtInput;
	public Slider AddRowAtSlider;

	public TMP_InputField RemoveColumnAtInput;
	public Slider RemoveColumnAtSlider;

	public TMP_InputField RemoveRowAtInput;
	public Slider RemoveRowAtSlider;

	public TMP_Text ValidText;



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
		AddColumnAtSlider.maxValue = leb.CurrentMap.Width;
		AddRowAtSlider.maxValue = leb.CurrentMap.Height;

		RemoveColumnAtSlider.maxValue = leb.CurrentMap.Width - 1;
		RemoveRowAtSlider.maxValue = leb.CurrentMap.Height - 1;

		// Map size validation
		try {
			int Width = int.Parse(WidthInput.text);
			if(Width > leb.maxSize) {
				WidthInput.text = leb.maxSize + "";
			}
			if(Width < leb.minSize) {
				WidthInput.text = leb.minSize + "";
			}
		} catch(FormatException e) {

		}

		try {
			int Height = int.Parse(HeightInput.text);
			if(Height > leb.maxSize) {
				HeightInput.text = leb.maxSize + "";
			}
			if(Height < leb.minSize) {
				HeightInput.text = leb.minSize + "";
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

		// Set Validation Text
		if(leb.CurrentMap.IsValid()) {
			ValidText.text = "";
		} else {
			ValidText.text = "Map is not Valid";
		}
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
		if(LastWidth.ToString() == WidthInput.text) {
			WidthInput.text = leb.CurrentMap.Width.ToString();
		} else {
			try {
				int Width = int.Parse(WidthInput.text);
				while(Width > leb.CurrentMap.Width) {
					leb.CurrentMap.AddColumn(leb.CurrentMap.Width);
				}
				while(Width < leb.CurrentMap.Width) {
					leb.CurrentMap.RemoveColumn(leb.CurrentMap.Width - 1);
				}
				leb.UpdateMap();
			} catch(FormatException e) {

			}
		}
		LastWidth = leb.CurrentMap.Width;

		if(LastHeight.ToString() == HeightInput.text) {
			HeightInput.text = leb.CurrentMap.Height.ToString();
		} else {
			try {
				int Height = int.Parse(HeightInput.text);
				while(Height > leb.CurrentMap.Height) {
					leb.CurrentMap.AddRow(leb.CurrentMap.Height);
				}
				while(Height < leb.CurrentMap.Height) {
					leb.CurrentMap.RemoveRow(leb.CurrentMap.Height - 1);
				}
				leb.UpdateMap();
			} catch(FormatException e) {

			}
		}
		LastHeight = leb.CurrentMap.Height;
	}
}
