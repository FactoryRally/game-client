using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Keybinding {

	public string Name = "Binding";
	public KeyCode PositiveButton;
	public KeyCode NegativeButton;
	public KeyCode AltPositiveButton;
	public KeyCode AltNegativeButton;
	public float Gravity = 5f;
	public float Sensitivity = 2.5f;
	public bool Snap = false;

	private float AxisValue = 0;
	private double DownTimePositive = 0;
	private double DownTimeNegative = 0;

	public Keybinding(string name) {
		this.Name = name;
		PositiveButton = (KeyCode) PlayerPrefs.GetInt("Binding." + Name + ".Positive", (int) PositiveButton);
		NegativeButton = (KeyCode) PlayerPrefs.GetInt("Binding." + Name + ".Negative", (int) NegativeButton);
		AltPositiveButton = (KeyCode) PlayerPrefs.GetInt("Binding." + Name + ".AltPositive", (int) AltPositiveButton);
		AltNegativeButton = (KeyCode) PlayerPrefs.GetInt("Binding." + Name + ".AltNegative", (int) AltNegativeButton);
		Gravity = PlayerPrefs.GetFloat("Binding." + Name + ".Gravity", Gravity);
		Sensitivity = PlayerPrefs.GetFloat("Binding." + Name + ".Sensitivity", Sensitivity);
		Snap = PlayerPrefs.GetInt("Binding." + Name + ".Snap", Snap ? 1 : 0) == 1 ? true : false;
	}


	public float GetAxis() {
		float value = AxisValue;
		bool negative = Input.GetKey(NegativeButton) || Input.GetKey(AltNegativeButton);
		bool positive = Input.GetKey(PositiveButton) || Input.GetKey(AltPositiveButton);

		if(negative && DownTimeNegative == 0)
			DownTimeNegative = Time.time;
		else if(!negative)
			DownTimeNegative = 0;

		if(positive && DownTimePositive == 0)
			DownTimePositive = Time.time;
		else if(!positive)
			DownTimePositive = 0;


		if(negative && (DownTimeNegative < DownTimePositive || DownTimePositive == 0)) {
			if(Snap && value > 0)
				value = 0;
			if(value > 0)
				value -= Time.deltaTime * Gravity;
			else
				value -= Time.deltaTime * Sensitivity;
		}
		if(positive && (DownTimePositive < DownTimeNegative || DownTimeNegative == 0)) {
			if(Snap && value < 0)
				value = 0;
			if(value < 0)
				value += Time.deltaTime * Gravity;
			else
				value += Time.deltaTime * Sensitivity;
		}
		if(!positive && !negative) {
			if(value > 0)
				value -= Time.deltaTime * Gravity;
			if(value < 0)
				value += Time.deltaTime * Gravity;
			if(Time.deltaTime * Gravity > Math.Abs(value))
				value = 0;
		}

		value = Mathf.Clamp(value, -1, 1);
		AxisValue = value;
		return value;
	}

	public bool GetButton() {
		return Input.GetKey(NegativeButton) || Input.GetKey(AltNegativeButton)
			|| Input.GetKey(PositiveButton) || Input.GetKey(AltPositiveButton);
	}

	public bool GetButtonDown() {
		return Input.GetKeyDown(NegativeButton) || Input.GetKeyDown(AltNegativeButton)
			|| Input.GetKeyDown(PositiveButton) || Input.GetKeyDown(AltPositiveButton);
	}

	public bool GetButtonUp() {
		return Input.GetKeyUp(NegativeButton) || Input.GetKeyUp(AltNegativeButton)
			|| Input.GetKeyUp(PositiveButton) || Input.GetKeyUp(AltPositiveButton);
	}

	public void Save() {
		PlayerPrefs.SetInt("Binding." + Name + ".Positive", (int) PositiveButton);
		PlayerPrefs.SetInt("Binding." + Name + ".Negative", (int) NegativeButton);
		PlayerPrefs.SetInt("Binding." + Name + ".AltPositive", (int) AltPositiveButton);
		PlayerPrefs.SetInt("Binding." + Name + ".AltNegative", (int) AltNegativeButton);
		PlayerPrefs.SetFloat("Binding." + Name + ".Gravity", Gravity);
		PlayerPrefs.SetFloat("Binding." + Name + ".Sensitivity", Sensitivity);
		PlayerPrefs.SetInt("Binding." + Name + ".Snap", Snap ? 1 : 0);
		PlayerPrefs.Save();
	}

	public static void Delete(string bindingname) {
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".Positive");
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".Negative");
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".AltPositive");
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".AltNegative");
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".Gravity");
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".Sensitivity");
		PlayerPrefs.DeleteKey("Binding." + bindingname + ".Snap");
		PlayerPrefs.Save();
	}

}
