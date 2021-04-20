using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboRally.Scripts.Utils {
	public class Settings {

		public Settings() {
			if(_instance == null)
				Settings._instance = this;
		}
		private static Settings _instance;
		public static Settings Instance {
			get {
				return _instance;
			}
			set {
				_instance = value;
			}
		}

		public bool IsDebug = false;

	}
}
