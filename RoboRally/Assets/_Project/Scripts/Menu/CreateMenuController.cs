using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

class CreateMenuController : MenuParent {

	public TMP_InputField GameNameInput;
	public TMP_InputField PasswordInput;
	public Slider MaxPlayersSlider;
	public Toggle ShowPlayerNamesToggle;
	public Toggle FillAiToggle;


	public void Awake() {

	}

	public void Start() {

	}

	public void Update() {

	}
	
	
	public void PressedCreateLobby() {
		Http.serverPath = Application.dataPath + "/Server/Tgm.Roborally.Server.exe";
		Http.StartServer();
		StartCoroutine(CreateLobby());
	}

	private IEnumerator CreateLobby() {
		// if(Http.running) {
		UnityWebRequest response = null;
		string[] body = {
			"player-names-visible=" + ShowPlayerNamesToggle.isOn.ToString().ToLower(),
			"max-players=" + MaxPlayersSlider.value,
			"name=" + GameNameInput.text,
			"robots-per-player=1",
			"password=" + PasswordInput.text,
			"fill-with-bots=" + FillAiToggle.isOn.ToString().ToLower()
		};
		yield return StartCoroutine(Http.Post("games", body, (x) => response = x));
		/* } else {
			UnityEngine.Debug.Log("Cannot reach Server");
		} */
	}

}
