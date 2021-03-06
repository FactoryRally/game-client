using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabSelect : MonoBehaviour {

	public EventSystem system;
	Selectable next = null;
	public int layer = 0;


	void Start() {
		system = EventSystem.current;
		next = GetComponent<Selectable>();
	}

	void Update() {
		if(system.currentSelectedGameObject != gameObject)
			return;

		bool isShift = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
		while(next != null && next.interactable) {
			if(Input.GetKeyDown(KeyCode.Tab) && !isShift) {
				next = next.FindSelectableOnDown();
			} else if(Input.GetKeyDown(KeyCode.Tab) && isShift) {
				next = next.FindSelectableOnUp();
			}

			if(next != null) {
				if(!next.GetComponent<TabSelect>() || layer != next.GetComponent<TabSelect>().layer)
					break;

				InputField inputfield = next.GetComponent<InputField>();
				TMP_InputField inputfieldTMP = next.GetComponent<TMP_InputField>();
				if(inputfield != null) {
					inputfield.OnPointerClick(new PointerEventData(system));
				} else if(inputfieldTMP != null) {
					inputfieldTMP.OnPointerClick(new PointerEventData(system));
				}
				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
				break;
			} else {
				break;
			}
		}
		next = GetComponent<Selectable>();
	}
}
