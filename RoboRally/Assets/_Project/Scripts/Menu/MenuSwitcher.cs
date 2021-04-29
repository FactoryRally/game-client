using RoboRally.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoboRally.Menu {
	public class MenuSwitcher : MonoBehaviour {

		public static MenuSwitcher instance = null;

		public Transform Container;
		public List<GameObject> MenuStack = new List<GameObject>();

		public GameObject MainMenu;
		public GameObject JoinMenu;
		public GameObject HostMenu;
		public GameObject CreateMenu;
		public GameObject RuleBookMenu;
		public GameObject SettingsMenu;

		private bool locked = false;


		public void Awake() {
			if(instance == null) {
				instance = this;
			} else {
				Destroy(gameObject);
			}

			MainMenu.SetActive(true);
			JoinMenu.SetActive(false);
			HostMenu.SetActive(false);
			CreateMenu.SetActive(false);
			RuleBookMenu.SetActive(false);
			SettingsMenu.SetActive(false);

			MenuStack = new List<GameObject>();
			MenuStack.Add(MainMenu);
		}

		public void Start() {

		}

		public void Update() {
			if(Input.GetKeyDown(KeyCode.Escape)) {
				Back();
			}
		}

		public void OpenJoinMenu() {
			if(locked)
				return;
			locked = true;
			MenuStack.Add(JoinMenu);
			Next();
		}

		public void OpenHostMenu() {
			if(locked)
				return;
			locked = true;
			MenuStack.Add(HostMenu);
			Next();
		}

		public void OpenCreateMenu() {
			if(locked)
				return;
			locked = true;
			MenuStack.Add(CreateMenu);
			Next();
		}

		public void OpenRuleBookMenu() {
			if(locked)
				return;
			locked = true;
			MenuStack.Add(RuleBookMenu);
			Next();
		}

		public void OpenSettingsMenu() {
			if(locked)
				return;
			locked = true;
			MenuStack.Add(SettingsMenu);
			Next();
		}

		public void OpenLevelEditor() {
			if(locked)
				return;
			locked = true;
			StartCoroutine(LoadLevelEditor());
		}

		public IEnumerator LoadLevelEditor() {
			if(locked)
				yield break;
			locked = true;
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelEditor");
			asyncLoad.allowSceneActivation = false;
			while(!asyncLoad.isDone) {
				if(asyncLoad.progress >= 0.9f) {
					asyncLoad.allowSceneActivation = true;
				}
				yield return null;
			}
		}

		public void QuitGame() {
			Application.Quit();
		}

		private void Next() {
			new DelayAction(this, 0.01f, () => {
				if(MenuStack.Count < 1) {
					locked = false;
					return;
				}
				MenuStack[MenuStack.Count - 1].SetActive(true);
				if(MenuStack.Count < 2) {
					locked = false;
					return;
				}
				MenuStack[MenuStack.Count - 2].SetActive(false);
				locked = false;
			});
		}

		public void Back(bool warning = false) {
			if(locked)
				return;
			locked = true;
			// implement warning
			new DelayAction(this, 0.01f, () => {
				if(MenuStack.Count <= 1) {
					locked = false;
					return;
				}
				MenuStack[MenuStack.Count - 1].SetActive(false);
				MenuStack.RemoveAt(MenuStack.Count - 1);
				MenuStack[MenuStack.Count - 1].SetActive(true);
				locked = false;
			});
		}

	}
}