using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorController : MonoBehaviour {

	public LevelEditorCameraController MainCamera;
	public Transform FocusPoint;

	public LevelEditorBuilder leb;

	public void Start() {
		FocusCamera();
	}

	public void Update() {
		HandleInputs();

		MainCamera.CanMove = leb.CurrentTileIndex == -1;
	}

	public void HandleInputs() {
		bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		if(KeybindingsController.GetButtonDown("EditorFocus")) {
			FocusCamera();
		}
		if(KeybindingsController.GetButtonDown("EditorDeselectTile")) {
			Debug.Log("EditorDeselectTile");
			leb.CurrentTileIndex = -1;
		}
		if(KeybindingsController.GetButtonDown("EditorRotate")) {
			leb.RotateTile(isShift);
		}
	}

	public void FocusCamera() {
		MainCamera.transform.position = new Vector3(
			FocusPoint.position.x,
			MainCamera.transform.position.y,
			FocusPoint.position.z
		);
	}

}
