using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public List<Animator> GUIHideComponentAnimators = new List<Animator>();

	public Animator StartMenu;
	public float StartMenuWaitTime = 0.5f;
	public Animator CreateMenu;
	public float CreateMenuWaitTime = 0.5f;
	public Animator JoinMenu;
	public float JoinMenuWaitTime = 0.5f;
	public Animator RulebookMenu;
	public float RulebookMenuWaitTime = 0.5f;
	public Animator LevelEditorMenu;
	public float LevelEditorMenuWaitTime = 0.5f;
	public Animator SettingsMenu;
	public float SettingsMenuWaitTime = 0.5f;

	void Start() {

	}

	void Update() {

	}

	public void PressedStart() {
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

	public void PressedBackToStart() {
		Hide();
		new DelayAction(this, CreateMenuWaitTime, () => {
			StartMenu.ResetTrigger("Hide");
			StartMenu.SetTrigger("Show");
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
		new DelayAction(this, LevelEditorMenuWaitTime, () => {
			LevelEditorMenu.ResetTrigger("Hide");
			LevelEditorMenu.SetTrigger("Show");
		});
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
				animator.gameObject.GetComponent<Button>().interactable = false;
			} else {
				animator.SetTrigger("Hide");
			}
		}
	}
}
