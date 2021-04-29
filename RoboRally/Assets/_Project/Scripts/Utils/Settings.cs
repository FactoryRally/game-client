using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

		public int Frames = 60;

		public Resolution Res = new Resolution() { width = 1920, height = 1080 };

		public FullScreenMode ScreenMode = FullScreenMode.MaximizedWindow;

		public int TextureQuality = 3;

		public AnisotropicFiltering Filtering = AnisotropicFiltering.Enable;

		public int AntiAliasing = 2;

		public int VSync = 1;

	}
}
