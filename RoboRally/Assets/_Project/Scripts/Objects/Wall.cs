using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.Objects {
	public class Wall : MonoBehaviour {

		[ReadOnly]
		public Material Mat;
		public GameObject WallObject;
		public Vector2 Size = new Vector2();


		void Start() {

		}

		public void SetWidth(int width) {
			WallObject.transform.localScale = new Vector3(width, 11, 1);
		}
	}
}
