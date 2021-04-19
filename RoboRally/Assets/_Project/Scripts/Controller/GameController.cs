using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRally.Controller {
	public class GameController : MonoBehaviour {

		private static GameController _instance;
		public static GameController Instance { get { return _instance; } }

		void Start() {
			if(_instance != null && _instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				_instance = this;
			}
		}

		void Update() {


		}

	}
}