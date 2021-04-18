using RoboRally.Keybindings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.LevelEdior {
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
			if(InputManager.GetButtonDown("EditorFocus")) {
				MainCamera.Focus();
			}
			if(InputManager.GetButtonDown("EditorDeselectTile")) {
				leb.CurrentTileIndex = -1;
			}
			if(InputManager.GetButtonDown("EditorRotate")) {
				leb.RotateTile(isShift);
			}
			if(InputManager.GetButtonDown("EditorLevel")) {
				if(InputManager.GetAxis("EditorLevel") > 0) {
					leb.TileLevel++;
				} else if(InputManager.GetAxis("EditorLevel") < 0) {
					leb.TileLevel--;
				}
			}
		}

	}
}