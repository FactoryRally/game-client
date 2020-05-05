using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCameraController : MonoBehaviour {

	public Camera Cam;
	public Transform Grid;
	public bool CanMove = false;

	// Camera Movement
	public float speed = 10.0f;

	private bool Dragging = false;
	private Vector3 LastPosition;
	private Vector3 LastValidPosition;
	private Vector3 Origin;

	// Camera Zoom
	public float minZoom = 10f;
	public float maxZoom = 90f;
	public float sensitivity = 10f;

	// Camera Clamping
	private Bounds bounds;


	void Awake() {
		Cam = GetComponent<Camera>();
		LastValidPosition = transform.position;
	}


	void Update() {
		bounds = new Bounds(Grid.position, new Vector3(11, 0, 11));

		if(Input.GetMouseButtonDown(0) && CanMove || Input.GetMouseButtonDown(2)) {
			Dragging = true;
			LastPosition = transform.position;
			Origin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		if(Input.GetMouseButton(0) && CanMove || Input.GetMouseButton(2)) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Origin;
			pos = new Vector3(pos.x, pos.z, pos.y);
			transform.position = LastPosition + -pos * (speed + ((transform.position.y - minZoom)));

			if(IsGridInBounds()) {
				LastValidPosition = transform.position;
			} else {
				transform.position = LastValidPosition;
			}
		}
		if(Input.GetMouseButtonUp(0) && CanMove || Input.GetMouseButtonUp(2)) {
			Dragging = false;
		}
		if(Input.GetAxis("Mouse ScrollWheel") != 0) {
			float y = Input.GetAxis("Mouse ScrollWheel") * sensitivity * -1;
			y = Mathf.Clamp(transform.position.y + y, minZoom, maxZoom);
			Vector3 pos = new Vector3(
				transform.position.x,
				y,
				transform.position.z
			);
			transform.position = pos;
		}
	}

	public bool IsGridInBounds() {
		float camTop = transform.position.z + Grid.localScale.z / 2;
		float camRight = transform.position.x + Grid.localScale.x / 2;
		float camBottom = transform.position.z - Grid.localScale.z / 2;
		float camLeft = transform.position.x - Grid.localScale.x / 2;

		if(camTop < bounds.min.z)
			return false;
		if(camRight < bounds.min.x)
			return false;
		if(camBottom > bounds.max.z)
			return false;
		if(camLeft > bounds.max.x)
			return false;

		return true;
	}

	public void Focus() {
		transform.position = new Vector3(
			Grid.position.x,
			transform.position.y,
			Grid.position.z
		);
	}
}
