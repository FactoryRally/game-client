using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCameraController : MonoBehaviour {

	[MyBox.Separator("Other")]
	public Camera Cam;
	public Transform Grid;

	[MyBox.Separator("Movement")]
	public float speed = 10.0f;
	[MyBox.ReadOnly]
	public bool MouseOverGUI = false;
	[MyBox.ReadOnly]
	public bool MoveSelected = false;

	private bool Dragging = false;
	private Vector3 LastPosition;
	private Vector3 LastValidPosition;
	private Vector3 Origin;

	[MyBox.ReadOnly]
	public bool DefaultCursor = false;

	[MyBox.Separator("Zoom")]
	public float minZoom = 10f;
	public float maxZoom = 90f;
	public float sensitivity = 10f;

	[MyBox.Separator("Cursor")]
	public Texture2D HandOpenCursor;
	public Texture2D HandClosedCursor;

	// Camera Clamping
	private Bounds bounds;


	void Awake() {
		Cam = GetComponent<Camera>();
		LastValidPosition = transform.position;
	}


	void Update() {
		if(MouseOverGUI)
			return;
		bounds = new Bounds(Grid.position, new Vector3(11, 0, 11));

		if(!DefaultCursor) {
			if(Dragging) {
				Cursor.SetCursor(HandClosedCursor, Vector2.zero, CursorMode.Auto);
			} else {
				Cursor.SetCursor(HandOpenCursor, Vector2.zero, CursorMode.Auto);
			}
		}

		if(Input.GetMouseButtonDown(0) && MoveSelected || Input.GetMouseButtonDown(2)) {
			Dragging = true;
			LastPosition = transform.position;
			Origin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		if(Input.GetMouseButton(0) && MoveSelected || Input.GetMouseButton(2)) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Origin;
			pos = new Vector3(pos.x, pos.z, pos.y);
			transform.position = LastPosition + -pos * (speed + ((transform.position.y - minZoom)));

			if(IsGridInBounds()) {
				LastValidPosition = transform.position;
			} else {
				transform.position = LastValidPosition;
			}
		}
		if(Input.GetMouseButtonUp(0) && MoveSelected || Input.GetMouseButtonUp(2)) {
			Dragging = false;
		}
		if(!Dragging && Input.GetAxis("Mouse ScrollWheel") != 0) {
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
