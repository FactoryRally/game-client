using RoboRally.Keybindings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoboRally.Objects {
	public class CameraMovement : MonoBehaviour {

		public float speed = 10.0f; 
		public float speedH = 2.0f;
		public float speedV = 2.0f;

		public float ClampX = 10;
		public float ClampY = 10;
		public Vector2 ClampHeight = new Vector2(2, 15);

		[Range(0, 89)]
		public float MaxXAngle = 85;

		private float yaw = 0.0f;
		private float pitch = 0.0f;

		private bool IsCam = false;


		void Start() {
			transform.position = MapBuilder.Instance.GetCenter() + 4 * Vector3.up;
		}


		void Update() {
			if(SceneManager.GetSceneByName("Menu_Game").isLoaded) {
				IsCam = false;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				return;
			}
			if(InputManager.GetButtonDown("CameraToggle")) {
				IsCam = !IsCam;
				Cursor.visible = !IsCam;
				if(IsCam)
					Cursor.lockState = CursorLockMode.Locked;
				else
					Cursor.lockState = CursorLockMode.None;
			}
			if(!IsCam)
				return;
			ClampX = MapBuilder.Instance.SelectedMap.Width / 2 + MapBuilder.Instance.WallDistanceModifier - 1;
			ClampY = MapBuilder.Instance.SelectedMap.Height / 2 + MapBuilder.Instance.WallDistanceModifier - 1;
			Vector3 currentPos = transform.position;
			Vector3 center = MapBuilder.Instance.GetCenter();
			Vector3 movement = new Vector3();
			Vector3 forwardIgnoreY = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
			Vector3 leftIgnoreY = - new Vector3(transform.right.x, 0, transform.right.z).normalized;
			movement =
				forwardIgnoreY * InputManager.GetAxis("CameraWS") * speed * Time.deltaTime +
				Vector3.up * InputManager.GetAxis("CameraVertical") * speed / 1.5f * Time.deltaTime +
				leftIgnoreY * InputManager.GetAxis("CameraAD") * speed * Time.deltaTime;
			currentPos += movement;
			currentPos = new Vector3(
				Mathf.Clamp(currentPos.x, -ClampX + center.x, ClampX + center.x),
				Mathf.Clamp(currentPos.y, ClampHeight.x, ClampHeight.y),
				Mathf.Clamp(currentPos.z, -ClampY + center.z, ClampY + center.z)
			);
			transform.position = currentPos;

			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			pitch = Mathf.Clamp(pitch, -MaxXAngle, MaxXAngle);
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
	}
}
