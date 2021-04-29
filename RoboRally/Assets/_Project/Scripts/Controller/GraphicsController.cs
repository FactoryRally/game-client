using RoboRally.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.UI.Extensions;

namespace RoboRally.Controller {
	public class GraphicsController : MonoBehaviour {

		public static GraphicsController Instance = null;

		private UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset lwrp;

		#region Frames

		public GameObject FramesList;
		private ScrollSnap FramesScrollSnap;

		public static int DefaultFrames = 60;
		public static int CurrentFrames;
		public static int[] PossibleFrames = {
			30,
			60,
			90,
			120
		};


		#endregion

		#region Resolution

		public GameObject ResolutionTextPrefab;
		public GameObject ResolutionList;
		private ScrollSnap ResolutionScrollSnap;

		public static Resolution DefaultResolution;
		public static Resolution CurrentResolution;
		public static Resolution[] PossibleResolutions = new Resolution[0];

		#endregion

		#region FullscreenMode

		public GameObject WindowModeList;
		private ScrollSnap WindowModeScrollSnap;

		public static FullScreenMode DefaultWindowMode = FullScreenMode.FullScreenWindow;
		public static FullScreenMode CurrentWindowMode;
		public static FullScreenMode[] PossibleWindowModes = {
		FullScreenMode.Windowed,
		FullScreenMode.FullScreenWindow,
		FullScreenMode.ExclusiveFullScreen
	};


		#endregion

		#region TextureQuality

		public GameObject TextureQualityList;
		private ScrollSnap TextureQualityScrollSnap;

		public static int DefaultTextureQuality = 3; // 0 = Low, 1 = Medium, 2 = High, 3 = Ultra
		public static int CurrentTextureQuality;

		#endregion

		#region AnisotropicFiltering

		public GameObject AnisotropicFilteringModeList;
		private ScrollSnap AnisotropicFilteringModeScrollSnap;

		public static AnisotropicFiltering DefaultAnisotropicFilteringMode = AnisotropicFiltering.Enable;
		public static AnisotropicFiltering CurrentAnisotropicFilteringMode;
		public static AnisotropicFiltering[] PossibleAnisotropicFilteringModes = {
		AnisotropicFiltering.Disable,
		AnisotropicFiltering.Enable,
	};

		#endregion

		#region AntiAliasing

		public GameObject AntiAliasingList;
		private ScrollSnap AntiAliasingScrollSnap;

		public static int DefaultAntiAliasing = 3; // 0 = Disabled, 1 = x2, 2 = x4, 3 = x8
		public static int CurrentAntiAliasing;

		#endregion

		#region VSync

		public GameObject VSyncList;
		private ScrollSnap VSyncScrollSnap;

		public static int DefaultVSync = 1; // 0 = Disabled, 1 = Enabled
		public static int CurrentVSync;

		#endregion

		private bool Changes = true;

		void Awake() {
			if(Instance == null) {
				Instance = this;
				DontDestroyOnLoad(this);
			} else {
				Destroy(gameObject);
			}
		}

		void Start() {
			lwrp = GraphicsSettings.renderPipelineAsset as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
			DefaultResolution = Screen.currentResolution;
			PossibleResolutions = Screen.resolutions;
			int i = 0;
			foreach(Transform t in ResolutionList.transform) {
				Destroy(t);
			}
			foreach(Resolution res in PossibleResolutions) {
				GameObject resolutionText = ResolutionTextPrefab;
				resolutionText.GetComponent<TMP_Text>().text = res.width + " x " + res.height;
				resolutionText = Instantiate(resolutionText, ResolutionList.transform);
				resolutionText.name = "resolution " + i;
				i++;
			}

			try {
				FramesScrollSnap = FramesList.transform.parent.gameObject.GetComponent<ScrollSnap>();
				ResolutionScrollSnap = ResolutionList.transform.parent.gameObject.GetComponent<ScrollSnap>();
				WindowModeScrollSnap = WindowModeList.transform.parent.gameObject.GetComponent<ScrollSnap>();
				TextureQualityScrollSnap = TextureQualityList.transform.parent.gameObject.GetComponent<ScrollSnap>();
				AnisotropicFilteringModeScrollSnap = AnisotropicFilteringModeList.transform.parent.gameObject.GetComponent<ScrollSnap>();
				AntiAliasingScrollSnap = AntiAliasingList.transform.parent.gameObject.GetComponent<ScrollSnap>();
				VSyncScrollSnap = VSyncList.transform.parent.gameObject.GetComponent<ScrollSnap>();
			} catch(Exception) {
				Debug.Log("Not all lists are given!");
			}

			SetDefault(); // Set default values 
			LoadGraphicSettings(); // If valid values are saved load them
			ApplyChanges(); // Apply everything
		}

		void Update() {
			if(Changes) {
				UpdateGUI();
			} else {
				UpdateValues();
			}
			Changes = !IsUpdated();

			if(Input.GetKeyDown(KeyCode.F11)) {
				if(CurrentWindowMode == FullScreenMode.Windowed) {
					CurrentWindowMode = FullScreenMode.FullScreenWindow;
				} else {
					CurrentWindowMode = FullScreenMode.Windowed;
				}
				ApplyChanges();
			}
		}

		public bool IsUpdated() {
			return (
				CurrentFrames == PossibleFrames[FramesScrollSnap.CurrentPage()] &&
				CurrentResolution.Equals(PossibleResolutions[ResolutionScrollSnap.CurrentPage()]) &&
				CurrentWindowMode == PossibleWindowModes[WindowModeScrollSnap.CurrentPage()] &&
				CurrentTextureQuality == TextureQualityScrollSnap.CurrentPage() &&
				CurrentAnisotropicFilteringMode == (AnisotropicFiltering) AnisotropicFilteringModeScrollSnap.CurrentPage() &&
				CurrentAntiAliasing == AntiAliasingScrollSnap.CurrentPage() &&
				CurrentVSync == VSyncScrollSnap.CurrentPage()
			);
		}

		public void UpdateValues() {
			CurrentFrames = PossibleFrames[FramesScrollSnap.CurrentPage()];
			CurrentResolution = PossibleResolutions[ResolutionScrollSnap.CurrentPage()];
			CurrentWindowMode = PossibleWindowModes[WindowModeScrollSnap.CurrentPage()];
			CurrentTextureQuality = TextureQualityScrollSnap.CurrentPage();
			CurrentAnisotropicFilteringMode = (AnisotropicFiltering) AnisotropicFilteringModeScrollSnap.CurrentPage();
			CurrentAntiAliasing = AntiAliasingScrollSnap.CurrentPage();
			CurrentVSync = VSyncScrollSnap.CurrentPage();
		}

		public void UpdateGUI() {
			int index = 0;
			if(FramesScrollSnap != null) {
				index = Array.IndexOf(PossibleFrames, CurrentFrames);
				FramesScrollSnap.JumpToPage(index);
			}
			if(ResolutionScrollSnap != null) {
				index = Array.IndexOf(PossibleResolutions, CurrentResolution);
				ResolutionScrollSnap.JumpToPage(index);
			}
			if(WindowModeScrollSnap != null) {
				index = Array.IndexOf(PossibleWindowModes, CurrentWindowMode);
				WindowModeScrollSnap.JumpToPage(index);
			}
			if(TextureQualityScrollSnap != null) {
				index = CurrentTextureQuality;
				TextureQualityScrollSnap.JumpToPage(index);
			}
			if(AnisotropicFilteringModeScrollSnap != null) {
				index = Array.IndexOf(PossibleAnisotropicFilteringModes, CurrentAnisotropicFilteringMode);
				AnisotropicFilteringModeScrollSnap.JumpToPage(index);
			}
			if(AntiAliasingScrollSnap != null) {
				index = CurrentAntiAliasing;
				AntiAliasingScrollSnap.JumpToPage(index);
			}
			if(AntiAliasingScrollSnap != null) {
				index = CurrentVSync;
				VSyncScrollSnap.JumpToPage(index);
			}
		}

		public void SetDefault() {
			CurrentFrames = DefaultFrames;
			CurrentResolution = DefaultResolution;
			CurrentWindowMode = DefaultWindowMode;
			CurrentTextureQuality = DefaultTextureQuality;
			CurrentAnisotropicFilteringMode = DefaultAnisotropicFilteringMode;
			CurrentAntiAliasing = DefaultAntiAliasing;
			CurrentVSync = DefaultVSync;

			UpdateGUI();
		}

		public void ApplyChanges() {
			Application.targetFrameRate = CurrentFrames;
			Screen.SetResolution(CurrentResolution.width, CurrentResolution.height, CurrentWindowMode);
			QualitySettings.masterTextureLimit = 3 - CurrentTextureQuality;
			QualitySettings.anisotropicFiltering = CurrentAnisotropicFilteringMode;
			lwrp.msaaSampleCount = (int) (Mathf.Pow(2, CurrentAntiAliasing) % 2 == 0 ? Mathf.Pow(2, CurrentAntiAliasing) : 0);
			QualitySettings.vSyncCount = CurrentVSync;

			SaveGarphicSettings();
		}


		public void SaveGarphicSettings() {
			Settings.Instance.Frames = CurrentFrames;
			Settings.Instance.Res = CurrentResolution;
			Settings.Instance.ScreenMode = CurrentWindowMode;
			Settings.Instance.TextureQuality = CurrentTextureQuality;
			Settings.Instance.Filtering = CurrentAnisotropicFilteringMode;
			Settings.Instance.AntiAliasing = CurrentAntiAliasing;
			Settings.Instance.VSync = CurrentVSync;
			SettingsController.Instance.SaveSettings();
		}

		public void LoadGraphicSettings() {
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