using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCameraController : MonoBehaviour {

	public Camera Cam;
	public bool CanMove = false;

	public float speed = 10.0f;

	private bool Dragging = false;
	private Vector3 LastPosition;
	private Vector3 Origin;

	void Awake() {
		Cam = GetComponent<Camera>();
	}


	void Update() {
		if(!CanMove) {
			Dragging = false;
			return;
		}

		if(Input.GetMouseButtonDown(0)) {
			Dragging = true;
			LastPosition = transform.position;
			Origin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		if(Input.GetMouseButton(0)) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Origin;
			pos = new Vector3(pos.x, pos.z, pos.y);
			transform.position = LastPosition + -pos * speed;
		}
		if(Input.GetMouseButtonUp(0)) {
			Dragging = false;
		}
	}
}
