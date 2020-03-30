using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LinkSliderInput : MonoBehaviour {

	public Slider slider;
	public TMP_InputField inputField;

	private float lastSliderValue;

	void Start() {
		lastSliderValue = slider.value;
	}

	public void UpdateValue() {
		if(inputField.text.Length == 0)
			return;
		try {
			if(lastSliderValue != float.Parse(inputField.text)) {
				slider.value = float.Parse(inputField.text);
				lastSliderValue = slider.value;
			} else {
				inputField.text = slider.value.ToString();
			}
		} catch(FormatException) { }
	}

	public void UpdateValueEnd() {

		try {
			if(inputField.text.Length == 0) {
				slider.value = lastSliderValue;
			}
			else {
				if(float.Parse(inputField.text) > slider.maxValue) {
					slider.value = slider.maxValue;
				}
				if(float.Parse(inputField.text) < slider.minValue) {
					slider.value = slider.minValue;
				}
			}
		} catch(FormatException) { }
		lastSliderValue = slider.value;
		inputField.text = slider.value.ToString();
	}

	public void HandleEmpty() {
		if(inputField.text.Length == 0)
			inputField.text = slider.value.ToString();
	}
}