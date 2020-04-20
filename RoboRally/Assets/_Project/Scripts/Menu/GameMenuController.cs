﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMenuControllerAction {
	Continue,
	Settings,
	Back,
	BackToGameMenu
}

[SerializeField]
public class GameMenuController : MonoBehaviour {

	public List<MenuComponent> MenuComponents = new List<MenuComponent>();

	public AudioClip ClipOnShow;
	public AudioSource Source;
	public bool IsLoaded = false;

	public void Awake() {
		Source = gameObject.GetComponent<AudioSource>();
	}

	void Start() {
		foreach(MenuComponent menuComponent in MenuComponents) {
			menuComponent.ComponentAnimator.gameObject.SetActive(true);
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape) && !IsLoaded) {
			Source.PlayOneShot(ClipOnShow);
			Show("Main");
			IsLoaded = true;
		} else if (Input.GetKeyDown(KeyCode.Escape) && IsLoaded) {
			Hide();
			IsLoaded = false;
		}
	}

	public void PressedButton(int action) {
		GameMenuControllerAction gmcAction = (GameMenuControllerAction) action;
		switch(gmcAction) {
			case GameMenuControllerAction.Continue:
				new DelayAction(this, 0.3f, () => {
					Hide();
				});
				new DelayAction(this, 1f, () => {
					if(SceneManager.GetSceneByBuildIndex(2).isLoaded) {
						SceneManager.UnloadSceneAsync(1);
					} else if(SceneManager.GetSceneByBuildIndex(3).isLoaded) {
						SceneManager.UnloadSceneAsync(1);
					}
					IsLoaded = false;
				});
				break;

			case GameMenuControllerAction.Settings:
				Hide();
				Show("Settings");
				break;

			case GameMenuControllerAction.Back:
				IsLoaded = false;
				AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
				asyncOperation.allowSceneActivation = false;
				new DelayAction(this, 0.5f, () => {
					asyncOperation.allowSceneActivation = true;
				});
				break;

			case GameMenuControllerAction.BackToGameMenu:
				Hide();
				Show("Main");
				break;
		}
	}

	public void Show(string name) {
		foreach(MenuComponent menuComponent in MenuComponents) {
			if(menuComponent.ComponentAnimator == null)
				continue;
			if(menuComponent.Name == name) {
				new DelayAction(this, menuComponent.WaitBefore, () => {
					menuComponent.ComponentAnimator.ResetTrigger("Hide");
					menuComponent.ComponentAnimator.SetTrigger("Show");
				});
			}
		}
	}

	public void Hide() {
		foreach(MenuComponent menuComponent in MenuComponents) {
			if(menuComponent.ComponentAnimator == null)
				continue;
			menuComponent.ComponentAnimator.SetTrigger("Hide");
		}
	}
}