using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour {

	private GameObject[] Buttons;
	public int index = 0;
	public Color SelectedColor;
	public Color UnselectedColor;

	void Start() {
		int s = 0;
		foreach(Transform child in transform) {
			s++;
		}
		if(s > 0) {
			Buttons = new GameObject[s];
			s = 0;
			foreach(Transform child in transform) {
				Buttons[s] = child.gameObject;
				Button b = Buttons[s].GetComponent<Button>();
				int temp = s;
				if(b != null) {
					b.onClick.AddListener(() => PressedButton(temp));
				}
				s++;
			}
			UnselectedColor = Buttons[0].gameObject.GetComponent<Image>().color;
		}
	}

	void Update() {
		if(Buttons != null && Buttons.Length > 0) {
			for(int i = 0; i < Buttons.Length; i++) {
				if(i == index) {
					Buttons[i].gameObject.GetComponent<Image>().color = SelectedColor;
				} else {
					Buttons[i].gameObject.GetComponent<Image>().color = UnselectedColor;
				}
			}
		}
	}

	public void PressedButton(int value) {
		if(value >= 0 && value < Buttons.Length)
			index = value;
	}
}
