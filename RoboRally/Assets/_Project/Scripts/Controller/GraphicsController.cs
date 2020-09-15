using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using UnityEngine.UI.Extensions;

public class GraphicsController : MonoBehaviour {

	private UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset lwrp;

	#region FullscreenMode

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
	public static Resolution[] PossibleResolutions;

	#endregion

	#region FullscreenMode

	public GameObject WindowModeList;
	private ScrollSnap WindowModeScrollSnap;

	public static FullScreenMode DefaultWindowMode = FullScreenMode.Windowed;
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


	void Start() {
		lwrp = GraphicsSettings.renderPipelineAsset as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
		DefaultResolution = Screen.currentResolution;
		PossibleResolutions = Screen.resolutions;
		int i = 0;
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
		LoadGarphicSettings(); // If valid values are saved load them
		ApplyChanges(); // Apply everything
	}

	void Update() {
		if(Changes) {
			UpdateGUI();
		} else {
			UpdateValues();
		}
		Changes = !IsUpdated();
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
		QualitySettings.masterTextureLimit = CurrentTextureQuality;
		QualitySettings.anisotropicFiltering = CurrentAnisotropicFilteringMode;
		lwrp.msaaSampleCount = (int) (Mathf.Pow(2, CurrentAntiAliasing) % 2 == 0 ? Mathf.Pow(2, CurrentAntiAliasing) : 0);
		QualitySettings.vSyncCount = CurrentVSync;

		SaveGarphicSettings();
	}


	public void SaveGarphicSettings() {
		PlayerPrefs.SetInt("frames", CurrentFrames);
		PlayerPrefs.SetString("resolution", CurrentResolution.width + "x" + CurrentResolution.height);
		PlayerPrefs.SetInt("screenMode", (int) CurrentWindowMode);
		PlayerPrefs.SetInt("textureQuality", CurrentTextureQuality);
		PlayerPrefs.SetInt("anisotropicFiltering", (int) CurrentAnisotropicFilteringMode);
		PlayerPrefs.SetInt("antiAliasing", (int) CurrentAntiAliasing);
		PlayerPrefs.SetInt("vSync", (int) CurrentVSync);
		PlayerPrefs.Save();
	}

	public void LoadGarphicSettings() {
		int frames = PlayerPrefs.GetInt("frames", -1);
		if(frames != -1) {
			CurrentFrames = frames;
		}

		string res = PlayerPrefs.GetString("resolution", null);
		if(res != null && res.IndexOf('x') > 0) {
			int xIndex = res.IndexOf('x');
			CurrentResolution.width = int.Parse(res.Substring(0, xIndex));
			CurrentResolution.height = int.Parse(res.Substring(xIndex + 1, res.Length - xIndex - 1));
		}

		int mode = PlayerPrefs.GetInt("screenMode", -1);
		if(mode != -1) {
			CurrentWindowMode = (FullScreenMode) mode;
		}

		int tq = PlayerPrefs.GetInt("textureQuality", -1);
		if(tq >= 0 && tq <= 3) {
			CurrentTextureQuality = tq;
		}

		int af = PlayerPrefs.GetInt("anisotropicFilter", -1);
		if(af != -1) {
			CurrentAnisotropicFilteringMode = (AnisotropicFiltering) af;
		}

		int aa = PlayerPrefs.GetInt("antiAliasing", -1);
		if(af != -1) {
			CurrentAntiAliasing = aa;
		}

		int vsync = PlayerPrefs.GetInt("vSync", -1);
		if(vsync >= 0 && vsync <= 1) {
			CurrentVSync = vsync;
		}
	}
}
