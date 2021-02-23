using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuSwitcher : MonoBehaviour {

	public Transform Container;
	public List<GameObject> MenuStack = new List<GameObject>();

	public GameObject MainMenu;
	public GameObject JoinMenu;
	public GameObject HostMenu;
	public GameObject CreateMenu;
	public GameObject RuleBookMenu;
	public GameObject SettingsMenu;


	public void Awake() {

	}

	public void Start() {

	}

	public void Update() {
		if(Input.GetKey(KeyCode.Escape)) {
			Back();
		}
	}


	public void OpenJoinMenu() {
		MenuStack.Add(JoinMenu);
		Next();
	}

	public void OpenHostMenu() {
		MenuStack.Add(HostMenu);
		Next();
	}

	public void OpenCreateMenu() {
		MenuStack.Add(CreateMenu);
		Next();
	}

	public void OpenRuleBookMenu() {
		MenuStack.Add(RuleBookMenu);
		Next();
	}

	public void OpenLevelEditor() {
		StartCoroutine(LoadLevelEditor());
	}

	public IEnumerator LoadLevelEditor() {
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelEditor");
		while(!asyncLoad.isDone) {
			yield return null;
		}
	}

	public void OpenSettingsBookMenu() {
		MenuStack.Add(SettingsMenu);
		Next();
	}

	public void QuitGame() {
		Application.Quit();
	}

	private void Next() {
		new DelayAction(this, 0.01f, () => {
			if(MenuStack.Count < 1)
				return;
			MenuStack[MenuStack.Count - 1].SetActive(true);
			if(MenuStack.Count < 2)
				return;
			MenuStack[MenuStack.Count - 2].SetActive(false);
		});
	}

	public void Back(bool warning = false) {
		// implement warning
		new DelayAction(this, 0.01f, () => {
			if(MenuStack.Count <= 1)
				return;
			MenuStack[MenuStack.Count - 1].SetActive(false);
			MenuStack.RemoveAt(MenuStack.Count - 1);
			MenuStack[MenuStack.Count - 1].SetActive(true);
		});
	}

}
