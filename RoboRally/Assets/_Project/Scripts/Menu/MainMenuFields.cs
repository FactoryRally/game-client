using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFields : MonoBehaviour {

	[MyBox.Separator("Create Lobby Fields")]
	public TMPro.TMP_InputField GameNameInput;
	public TMPro.TMP_InputField PasswordInput;
	public Slider MaxPlayersSlider;
	public Toggle ShowPlayerNamesToggle;
	public Toggle FillAiToggle;


	void Start() {

	}


	void Update() {

	}
}
