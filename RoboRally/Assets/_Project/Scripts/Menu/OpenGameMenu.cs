using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGameMenu : MonoBehaviour {

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(!SceneManager.GetSceneByName("Menu_Game").isLoaded) {
				SceneManager.LoadScene("Menu_Game", LoadSceneMode.Additive);
			}
		}
	}

}
