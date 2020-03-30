using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KeybindingsController : MonoBehaviour {

	public GameObject BindingsContainer;
	public Color DefaultColor;
	public Color BindingColor;

	public GameObject MouseClickPreventer;

	private GameObject CurrentButton;

	public static List<Keybinding> keybindings = new List<Keybinding>();
	public List<Keybinding> Keybindings = new List<Keybinding>();

	void Start() {
		SetDefault();
		LoadBindings();
	}

	void Update() {
		if(Application.isEditor) {
			SaveBindings();
		}
	}

	void OnGUI() {
		if(!Application.isPlaying)
			return;
		UpdateBindingsText();

		if(!Input.GetMouseButton(0) && MouseClickPreventer != null)
			MouseClickPreventer.SetActive(false);

		if(CurrentButton == null)
			return;

		MouseClickPreventer.SetActive(true);

		Event evt = Event.current;
		KeyCode key = evt.keyCode == KeyCode.Escape ? KeyCode.None : evt.keyCode;
		string bindingName = CurrentButton.transform.parent.name.ToLower();
		bool isAlt = bindingName.EndsWith("Alt");
		if(isAlt)
			bindingName = bindingName.Substring(0, bindingName.LastIndexOf("Alt"));


		if(evt.isKey || evt.shift) {
			if(Input.GetKey(KeyCode.LeftShift))
				key = KeyCode.LeftShift;
			else if(Input.GetKey(KeyCode.RightShift))
				key = KeyCode.RightShift;

		} else {
			for(int i = 0; i < 20; i++) {
				try {
					if(Input.GetMouseButton(i)) {
						key = KeyCode.Mouse0 + i;
					}
				} catch(ArgumentException) { }
			}
		}

		if(!isAlt) {
			GetBindingByName(bindingName).PositiveButton = key;
		} else {
			GetBindingByName(bindingName).AltPositiveButton = key;
		}

		ColorBlock colors = CurrentButton.GetComponent<Button>().colors;
		colors.normalColor = DefaultColor;
		CurrentButton.GetComponent<Button>().colors = colors;
		CurrentButton = null;
	}

	#region Key Methods

	public static float GetAxis(string keybindingName) {
		foreach(Keybinding binding in keybindings) {
			if(binding.Name.ToLower().Equals(keybindingName.ToLower()))
				return binding.GetAxis();
		}
		return 0;
	}

	#endregion

	#region GUI Methods

	public void SetBindings(GameObject gameObject) {
		CurrentButton = gameObject;

		ColorBlock colors = CurrentButton.GetComponent<Button>().colors;
		colors.normalColor = BindingColor;
		CurrentButton.GetComponent<Button>().colors = colors;
	}

	#endregion

	#region Binding Methods

	public void SaveBindings() {
		string bindingNames = "";
		foreach(Keybinding binding in keybindings) {
			binding.Save();
			bindingNames += binding.Name + ", ";
		}
		PlayerPrefs.SetString("Bindings", bindingNames);
		PlayerPrefs.Save();
	}

	public void LoadBindings() {
		string[] bindingNames = PlayerPrefs.GetString("Bindings", "").Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
		keybindings = new List<Keybinding>();
		foreach(string bindingName in bindingNames) {
			keybindings.Add(new Keybinding(bindingName));
		}
	}

	public void SetDefault() {

	}

	public Keybinding GetBindingByName(string bindingName) {
		foreach(Keybinding binding in keybindings) {
			if(binding.Name.Equals(bindingName, StringComparison.InvariantCultureIgnoreCase)) {
				return binding;
			}
		}
		return null;
	}

	#endregion

	#region private Methods

	private void UpdateBindingsText() {
		if(BindingsContainer == null)
			return;
		for(int i = 0; i < BindingsContainer.transform.childCount; i++) {
			GameObject binding = BindingsContainer.transform.GetChild(i).gameObject;
			if(binding != null) {
				Transform binding1 = binding.transform.Find("Binding 1");
				TMP_Text text1 = binding1.Find("Text").gameObject.GetComponent<TMP_Text>();
				string name1 = binding1.parent.name.ToLower() + binding1.name[binding1.name.Length - 1];
				text1.text = GetBindingByName(name1).ToString();

				Transform binding2 = binding.transform.Find("Binding 2");
				TMP_Text text2 = binding2.Find("Text").gameObject.GetComponent<TMP_Text>();
				string name2 = binding2.parent.name.ToLower() + binding2.name[binding2.name.Length - 1];
				text2.text = GetBindingByName(name2).ToString();
			}
		}
	}

	#endregion

}
