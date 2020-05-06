using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorController : MonoBehaviour {

	public LevelEditorCameraController MainCamera;

	public LevelEditorBuilder leb;

	void Awake() {
		leb = GetComponent<LevelEditorBuilder>();
	}

	public void Start() {
		MainCamera.Focus();
	}

	public void Update() {
		HandleInputs();

		MainCamera.MoveSelected = leb.MoveSelected;
		MainCamera.MouseOverGUI = leb.MouseOverGUI;
	}

	public void HandleInputs() {
		bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		if(KeybindingsController.GetButtonDown("EditorFocus")) {
			MainCamera.Focus();
		}
		if(KeybindingsController.GetButtonDown("EditorDeselectTile")) {
			leb.CurrentTileIndex = -1;
		}
		if(KeybindingsController.GetButtonDown("EditorRotate")) {
			leb.RotateTile(isShift);
		}
		if(KeybindingsController.GetButtonDown("EditorLevel")) {
			if(KeybindingsController.GetAxis("EditorLevel") > 0) {
				leb.TileLevel++;
			} else if(KeybindingsController.GetAxis("EditorLevel") < 0) {
				leb.TileLevel--;
			}
		} 
	}

}
