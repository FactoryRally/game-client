using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;

namespace RoboRally.Objects {
	public class Robot : MonoBehaviour {

		public int RobotId;

		void Start() {
			FindObjectOfType<EventHandler>().OnMovement.AddListener(OnMoveEvent);
		}


		void Update() {

		}


		public void OnMoveEvent(MovementEvent ev) {
			Debug.Log("My Robot: " + RobotId + " Event Robot: " + RobotId);
			if(ev.Entity != RobotId)
				return;
			if(ev.Rotation != null) {
				for(int i = 0; i < ev.RotationTimes; i++)
					transform.rotation *= MapBuilder.RotationToQuaternion((Rotation) ev.Rotation);
			}
			Debug.Log("New Pos: " + new Vector3(ev.To.Y, 2, ev.To.X));
			transform.position = new Vector3(ev.To.Y, 2, ev.To.X);
		}
	}
}
