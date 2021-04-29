using RoboRally.Controller;
using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace RoboRally.Menu {
	public class GraphicsMenuController : MonoBehaviour {

		public GameObject FramesList;
		private ScrollSnap FramesScrollSnap;

		public GameObject ResolutionTextPrefab;
		public GameObject ResolutionList;
		private ScrollSnap ResolutionScrollSnap;

		public GameObject WindowModeList;
		private ScrollSnap WindowModeScrollSnap;

		public GameObject TextureQualityList;
		private ScrollSnap TextureQualityScrollSnap;

		public GameObject AnisotropicFilteringModeList;
		private ScrollSnap AnisotropicFilteringModeScrollSnap;

		public GameObject AntiAliasingList;
		private ScrollSnap AntiAliasingScrollSnap;

		public GameObject VSyncList;
		private ScrollSnap VSyncScrollSnap;

		public GameObject SettingsMenu;

		private bool Changes = true;
		


		void Awake() {

		}

		void Start() {
			int i = 0;
			foreach(Transform t in ResolutionList.transform) {
				Destroy(t);
			}
			foreach(Resolution res in GraphicSettings.PossibleResolutions) {
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
		}


		void Update() {
			if(!SettingsMenu || !SettingsMenu.activeSelf)
				return;
			if(Changes) {
				UpdateGUI();
			} else {
				UpdateValues();
			}
			Changes = !IsUpdated();
		}

		public bool IsUpdated() {
			return (
				GraphicSettings.CurrentFrames == GraphicSettings.PossibleFrames[FramesScrollSnap.CurrentPage()] &&
				GraphicSettings.CurrentResolution.Equals(GraphicSettings.PossibleResolutions[ResolutionScrollSnap.CurrentPage()]) &&
				GraphicSettings.CurrentWindowMode == GraphicSettings.PossibleWindowModes[WindowModeScrollSnap.CurrentPage()] &&
				GraphicSettings.CurrentTextureQuality == TextureQualityScrollSnap.CurrentPage() &&
				GraphicSettings.CurrentAnisotropicFilteringMode == (AnisotropicFiltering) AnisotropicFilteringModeScrollSnap.CurrentPage() &&
				GraphicSettings.CurrentAntiAliasing == AntiAliasingScrollSnap.CurrentPage() &&
				GraphicSettings.CurrentVSync == VSyncScrollSnap.CurrentPage()
			);
		}

		public void UpdateValues() {
			GraphicSettings.CurrentFrames = GraphicSettings.PossibleFrames[FramesScrollSnap.CurrentPage()];
			GraphicSettings.CurrentResolution = GraphicSettings.PossibleResolutions[ResolutionScrollSnap.CurrentPage()];
			GraphicSettings.CurrentWindowMode = GraphicSettings.PossibleWindowModes[WindowModeScrollSnap.CurrentPage()];
			GraphicSettings.CurrentTextureQuality = TextureQualityScrollSnap.CurrentPage();
			GraphicSettings.CurrentAnisotropicFilteringMode = (AnisotropicFiltering) AnisotropicFilteringModeScrollSnap.CurrentPage();
			GraphicSettings.CurrentAntiAliasing = AntiAliasingScrollSnap.CurrentPage();
			GraphicSettings.CurrentVSync = VSyncScrollSnap.CurrentPage();
		}

		public void UpdateGUI() {
			int index = 0;
			if(FramesScrollSnap != null) {
				index = Array.IndexOf(GraphicSettings.PossibleFrames, GraphicSettings.CurrentFrames);
				FramesScrollSnap.ChangePage(index);
			}
			if(ResolutionScrollSnap != null) {
				index = Array.IndexOf(GraphicSettings.PossibleResolutions, GraphicSettings.CurrentResolution);
				ResolutionScrollSnap.ChangePage(index);
			}
			if(WindowModeScrollSnap != null) {
				index = Array.IndexOf(GraphicSettings.PossibleWindowModes, GraphicSettings.CurrentWindowMode);
				WindowModeScrollSnap.ChangePage(index);
			}
			if(TextureQualityScrollSnap != null) {
				index = GraphicSettings.CurrentTextureQuality;
				TextureQualityScrollSnap.ChangePage(index);
			}
			if(AnisotropicFilteringModeScrollSnap != null) {
				index = Array.IndexOf(GraphicSettings.PossibleAnisotropicFilteringModes, GraphicSettings.CurrentAnisotropicFilteringMode);
				AnisotropicFilteringModeScrollSnap.ChangePage(index);
			}
			if(AntiAliasingScrollSnap != null) {
				index = GraphicSettings.CurrentAntiAliasing;
				AntiAliasingScrollSnap.ChangePage(index);
			}
			if(AntiAliasingScrollSnap != null) {
				index = GraphicSettings.CurrentVSync;
				VSyncScrollSnap.ChangePage(index);
			}
		}

		public void ResetUI() {
			GraphicSettings.LoadGraphicSettings();
			UpdateGUI();
		}

		public void Default() {
			GraphicSettings.SetDefault();
			UpdateGUI();
			GraphicSettings.ApplyChanges();
		}

		public void Apply() {
			GraphicSettings.ApplyChanges();
		}
	}
}