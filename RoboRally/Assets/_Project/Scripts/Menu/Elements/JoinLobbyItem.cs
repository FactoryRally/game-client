using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyItem : MonoBehaviour {

	public int GameID = 0;
	public bool IsLocked = false;

	public Image LockIcon;
	public TMP_Text GameNameText;
	public TMP_Text PlayersAmountText;

	public Sprite LockedImage;
	public Sprite UnlockedImage;

	private float lastTime = 0;
	private float waitTime = 5;

	public void Start() {
		
	}

	public void Update() {
		// Update GUI all 5 sec
		if(lastTime <= Time.time && GameID != 0) { 
			lastTime = Time.time + waitTime;
			UpdateGUI();
		}
	}

	
	public void UpdateGUI() {
		GameNameText.text = ""; // TODO: Get GameName from the game
		PlayersAmountText.text = ""; // TODO: Get maximum and current playerammount from the game
		if(IsLocked) {
			LockIcon.sprite = LockedImage;
		} else {
			LockIcon.sprite = UnlockedImage;
		}
	}

}