using RoboRally.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.UI.Extensions;

namespace RoboRally.Utils {
	public class GraphicSettings  {

		private static UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset lwrp;

		public static int DefaultFrames = 60;
		public static int CurrentFrames;
		public static int[] PossibleFrames = {
			30,
			60,
			90,
			120
		};

		public static Resolution DefaultResolution;
		public static Resolution CurrentResolution;
		public static Resolution[] PossibleResolutions = new Resolution[0];

		public static FullScreenMode DefaultWindowMode = FullScreenMode.FullScreenWindow;
		public static FullScreenMode CurrentWindowMode;
		public static FullScreenMode[] PossibleWindowModes = {
			FullScreenMode.ExclusiveFullScreen,
			FullScreenMode.FullScreenWindow,
			FullScreenMode.MaximizedWindow,
			FullScreenMode.Windowed
		};

		public static int DefaultTextureQuality = 3; // 0 = Low, 1 = Medium, 2 = High, 3 = Ultra
		public static int CurrentTextureQuality;

		public static AnisotropicFiltering DefaultAnisotropicFilteringMode = AnisotropicFiltering.Enable;
		public static AnisotropicFiltering CurrentAnisotropicFilteringMode;
		public static AnisotropicFiltering[] PossibleAnisotropicFilteringModes = {
			AnisotropicFiltering.Disable,
			AnisotropicFiltering.Enable,
		};

		public static int DefaultAntiAliasing = 3; // 0 = Disabled, 1 = x2, 2 = x4, 3 = x8
		public static int CurrentAntiAliasing;
		public static int DefaultVSync = 1; // 0 = Disabled, 1 = Enabled
		public static int CurrentVSync;

		public static void Setup() {
			lwrp = GraphicsSettings.renderPipelineAsset as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
			DefaultResolution = Screen.currentResolution;
			PossibleResolutions = Screen.resolutions;

			SetDefault(); // Set default values 
			LoadGraphicSettings(); // If valid values are saved load them
			ApplyChanges(); // Apply everything
		}

		public static void Update() {
			if(Input.GetKeyDown(KeyCode.F11)) {
				if(CurrentWindowMode == FullScreenMode.MaximizedWindow) {
					CurrentWindowMode = FullScreenMode.FullScreenWindow;
				} else {
					CurrentWindowMode = FullScreenMode.MaximizedWindow;
				}
				ApplyChanges();
			}
		}


		public static void SetDefault() {
			CurrentFrames = DefaultFrames;
			CurrentResolution = DefaultResolution;
			CurrentWindowMode = DefaultWindowMode;
			CurrentTextureQuality = DefaultTextureQuality;
			CurrentAnisotropicFilteringMode = DefaultAnisotropicFilteringMode;
			CurrentAntiAliasing = DefaultAntiAliasing;
			CurrentVSync = DefaultVSync;
		}

		public static void ApplyChanges() {
			Application.targetFrameRate = CurrentFrames;
			Screen.SetResolution(CurrentResolution.width, CurrentResolution.height, CurrentWindowMode);
			QualitySettings.masterTextureLimit = 3 - CurrentTextureQuality;
			QualitySettings.anisotropicFiltering = CurrentAnisotropicFilteringMode;
			lwrp.msaaSampleCount = (int) (Mathf.Pow(2, CurrentAntiAliasing) % 2 == 0 ? Mathf.Pow(2, CurrentAntiAliasing) : 0);
			QualitySettings.vSyncCount = CurrentVSync;

			SaveGarphicSettings();
		}


		public static void SaveGarphicSettings() {
			Settings.Instance.Frames = CurrentFrames;
			Settings.Instance.Res = CurrentResolution;
			Settings.Instance.ScreenMode = CurrentWindowMode;
			Settings.Instance.TextureQuality = CurrentTextureQuality;
			Settings.Instance.Filtering = CurrentAnisotropicFilteringMode;
			Settings.Instance.AntiAliasing = CurrentAntiAliasing;
			Settings.Instance.VSync = CurrentVSync;
			SettingsController.Instance.SaveSettings();
		}

		public static void LoadGraphicSettings() {
			CurrentFrames = Settings.Instance.Frames;
			CurrentResolution = Settings.Instance.Res;
			CurrentWindowMode = Settings.Instance.ScreenMode;
			CurrentTextureQuality = Settings.Instance.TextureQuality;
			CurrentAnisotropicFilteringMode = Settings.Instance.Filtering;
			CurrentAntiAliasing = Settings.Instance.AntiAliasing;
			CurrentVSync = Settings.Instance.VSync;
		}
	}
}