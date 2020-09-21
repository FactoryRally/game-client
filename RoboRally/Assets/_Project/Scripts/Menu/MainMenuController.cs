using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

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

	public MainMenuFields fields;
	private Process server;

	void Start() {
		MainMenu.gameObject.SetActive(true);
		StartMenu.gameObject.SetActive(true);
		CreateMenu.gameObject.SetActive(true);
		JoinMenu.gameObject.SetActive(true);
		RulebookMenu.gameObject.SetActive(true);
		SettingsMenu.gameObject.SetActive(true);
		fields = GetComponent<MainMenuFields>();
	}

	void OnApplicationQuit() {
		if(server != null) {
			try {
				server.Kill();
			} catch(Exception exc) {

			}
		}
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
		if(!Http.running) {
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = false;
			startInfo.FileName = "D:\\Coding\\Projekte\\FactoryRally\\game-controller\\server\\src\\Tgm.Roborally.Server\\bin\\Debug\\netcoreapp3.1\\Tgm.Roborally.Server.exe";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;

			try {
				server = Process.Start(startInfo);
				Http.running = true;
			} catch(Exception e) {
				UnityEngine.Debug.Log("Was not able to start the server!");
			}
		}
		StartCoroutine(CreateLobby());
	}

	private IEnumerator CreateLobby() {
		if(Http.running) {
			UnityWebRequest response = null;
			string[] body = {
				"player-names-visible=" + fields.ShowPlayerNamesToggle.isOn.ToString().ToLower(),
				"max-players=" + fields.MaxPlayersSlider.value,
				"name=" + fields.GameNameInput.text,
				"robots-per-player=1",
				"password=" + fields.PasswordInput.text,
				"fill-with-bots=" + fields.FillAiToggle.isOn.ToString().ToLower()
			};
			yield return StartCoroutine(Http.Post("games", body, response));
			UnityEngine.Debug.Log("HI2");
			UnityEngine.Debug.Log(response.responseCode);
		} else {
			UnityEngine.Debug.Log("Cannot reach Server");
		}
	}
	
	/*
	public void PressedCreateLobby() {
		if(!Http.running) {
			new DelayAction(
				this,
				0,
				() => {
					ProcessStartInfo startInfo = new ProcessStartInfo();
					startInfo.CreateNoWindow = false;
					startInfo.UseShellExecute = false;
					startInfo.FileName = "D:\\Coding\\Projekte\\FactoryRally\\game-controller\\server\\src\\Tgm.Roborally.Server\\bin\\Debug\\netcoreapp3.1\\Tgm.Roborally.Server.exe";
					startInfo.WindowStyle = ProcessWindowStyle.Hidden;

					try {
						server = Process.Start(startInfo);
						Http.running = true;
					} catch { }
				}
			);
		}
		DelayAction da = new DelayAction();
		new DelayAction(
			this,
			0.5f,
			10,
			() => {
				if(Http.running) {
					da.StopRepeat();
					DownloadHandler response = null;
					string[] body = {
						"player-names-visible=" + fields.ShowPlayerNamesToggle.isOn,
						"max-players=" + fields.MaxPlayersSlider.value,
						"name=" + fields.GameNameInput.text,
						"robots-per-player=1",
						"password=" + fields.PasswordInput.text,
						"fill-with-bots=" + fields.FillAiToggle.isOn
					};
					yield return StartCoroutine(Http.Post("games", body, response));

					UnityEngine.Debug.Log(response.isDone);
				} else if(da.currentRepeat == 10) {
					UnityEngine.Debug.Log("Cannot reach Server");
				}

			}
		);
	}
	*/

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
