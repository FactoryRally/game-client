using RoboRally.Keybindings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.Objects {
	public class CameraMovement : MonoBehaviour {

		public bool HideCursor = true;

		public float speed = 10.0f; 
		public float speedH = 2.0f;
		public float speedV = 2.0f;

		public float ClampX = 10;
		public float ClampY = 10;
		public Vector2 ClampHeight = new Vector2(2, 9);

		[Range(0, 89)]
		public float MaxXAngle = 85;

		private float yaw = 0.0f;
		private float pitch = 0.0f;


		void Start() {
			if(HideCursor)
				Cursor.lockState = CursorLockMode.Locked;
			transform.position = MapBuilder.Instance.GetCenter() + 4 * Vector3.up;
		}


		void Update() {
			ClampX = MapBuilder.Instance.SelectedMap.Width;
			ClampX = MapBuilder.Instance.SelectedMap.Height;
			Vector3 currentPos = transform.position;
			Vector3 center = MapBuilder.Instance.GetCenter();
			Vector3 movement = new Vector3();
			movement =
				transform.forward * InputManager.GetAxis("CameraWS") * speed * Time.deltaTime +
				transform.up * InputManager.GetAxis("CameraVertical") * speed / 1.5f * Time.deltaTime +
				transform.right * InputManager.GetAxis("CameraAD") * speed * Time.deltaTime;
			currentPos += movement;
			currentPos = new Vector3(
				Mathf.Clamp(currentPos.x, -ClampX + center.x, ClampX + center.z),
				Mathf.Clamp(currentPos.y, ClampHeight.x, ClampHeight.y),
				Mathf.Clamp(currentPos.z, -ClampY + center.x, ClampY + center.z)
			);
			transform.position = currentPos;

			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			pitch = Mathf.Clamp(pitch, -MaxXAngle, MaxXAngle);
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
	}
}
