using RoboRally.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace RoboRally.Menu {
	public class GameSettingsMenuController : MonoBehaviour {

		public GameObject DebugList;
		private ScrollSnap DebugScrollSnap;

		public GameObject SettingsMenu;

		private bool Changes = false;


		void Start() {
			int i = 0;
			try {
				DebugScrollSnap = DebugList.transform.parent.gameObject.GetComponent<ScrollSnap>();
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
				Settings.Instance.IsDebug == !(DebugScrollSnap.CurrentPage() == 0)
			);
		}

		public void UpdateValues() {
			Settings.Instance.IsDebug = !(DebugScrollSnap.CurrentPage() == 0);
		}

		public void UpdateGUI() {
			int index = 0;
			if(DebugScrollSnap != null) {
				index = Settings.Instance.IsDebug ? 1 : 0;
				DebugScrollSnap.ChangePage(index);
			}
		}

		public void ResetUI() {
			Settings.Instance.IsDebug = true;
			UpdateGUI();
		}

		public void Default() {
			Settings.Instance.IsDebug = false;
			UpdateGUI();
			SettingsController.Instance.SaveSettings();
		}

		public void Apply() {
			SettingsController.Instance.SaveSettings();
		}
	}
}
