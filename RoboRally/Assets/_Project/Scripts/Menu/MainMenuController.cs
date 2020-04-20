﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public List<Animator> GUIHideComponentAnimators = new List<Animator>();

	public GameObject MainMenu;
	public float MainMenuWaitTime = 0.5f;
	public Animator StartMenu;
	public float StartMenuWaitTime = 0.5f;
	public Animator CreateMenu;
	public float CreateMenuWaitTime = 0.5f;
	public Animator JoinMenu;
	public float JoinMenuWaitTime = 0.5f;
	public Animator RulebookMenu;
	public float RulebookMenuWaitTime = 0.5f;
	public Animator SettingsMenu;
	public float SettingsMenuWaitTime = 0.5f;

	void Start() {
		MainMenu.gameObject.SetActive(true);
		StartMenu.gameObject.SetActive(true);
		CreateMenu.gameObject.SetActive(true);
		JoinMenu.gameObject.SetActive(true);
		RulebookMenu.gameObject.SetActive(true);
		SettingsMenu.gameObject.SetActive(true);
	}

	void Update() {
		
	}

	public void PressedBackToMain() {
		Hide();
		new DelayAction(this, MainMenuWaitTime, () => {
			foreach(Animator animator in MainMenu.GetComponentsInChildren<Animator>()) {
				animator.ResetTrigger("Hide");
				animator.SetTrigger("Idle");
			}
			foreach(Button button in MainMenu.GetComponentsInChildren<Button>()) {
				button.interactable = true;
			}
		});
	}

	public void PressedStart() {
		Hide();
		new DelayAction(this, StartMenuWaitTime, () => {
			StartMenu.ResetTrigger("Hide");
			StartMenu.SetTrigger("Show");
		});
	}

	public void PressedBackToStart() {
		Hide();
		new DelayAction(this, StartMenuWaitTime, () => {
			StartMenu.ResetTrigger("Hide");
			StartMenu.SetTrigger("Show");
		});
	}

	public void PressedCreate() {
		Hide();
		new DelayAction(this, CreateMenuWaitTime, () => {
			CreateMenu.ResetTrigger("Hide");
			CreateMenu.SetTrigger("Show");
		});
	}

	public void PressedCreateLobby() {
		// TODO
	}

	public void PressedJoin() {
		Hide();
		new DelayAction(this, JoinMenuWaitTime, () => {
			JoinMenu.ResetTrigger("Hide");
			JoinMenu.SetTrigger("Show");
		});
	}

	public void PressedRulebook() {
		Hide();
		new DelayAction(this, RulebookMenuWaitTime, () => {
			RulebookMenu.ResetTrigger("Hide");
			RulebookMenu.SetTrigger("Show");
		});
	}

	public void PressedLevelEditor() {
		Hide();
		SceneManager.LoadScene(2);
		SceneManager.LoadScene(1, LoadSceneMode.Additive);
	}

	public void PressedSettings() {
		Hide();
		new DelayAction(this, SettingsMenuWaitTime, () => {
			SettingsMenu.ResetTrigger("Hide");
			SettingsMenu.SetTrigger("Show");
		});
	}

	public void PressedLeave() {
		Application.Quit(0);
	}

	public void Hide() {
		foreach(Animator animator in GUIHideComponentAnimators) {
			if(animator == null)
				continue;
			if(animator.gameObject.GetComponent<Button>() != null) {
				if(animator.gameObject.GetComponent<Button>().interactable == false)
					continue;
				int i = 0;
				animator.gameObject.SetActive(false);
				animator.gameObject.SetActive(true);
				animator.gameObject.GetComponent<Button>().interactable = false;
			} else {
				animator.SetTrigger("Hide");
			}
		}
	}
}