using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

	private Text TextField;
	public float DeleteAfter = 3;
	private DelayAction DelayAction1;

	void Start() {
		TextField = GetComponent<Text>();
		gameObject.tag = "DebugText";
	}


	void Update() {
		if(TextField.text != "") {
			
		}
	}

	public static DebugText Get() {
		return GameObject.FindGameObjectsWithTag("DebugText")[0].GetComponent<DebugText>();
	}

	public static void Set(string text) {
		DebugText.Get().SetText(text);
	}

	public void SetText(string text) {
		TextField.text = text;
		if(DelayAction1 != null)
			DelayAction1.Stop();
		DelayAction1 = new DelayAction(this, DeleteAfter, () => {
			TextField.text = "";
		});
	}
}
