using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoboRally.Menu.Elements {
	[ExecuteInEditMode]
	public class SidesView : MonoBehaviour {

		public GameObject MenuBar;
		public Color DefaultColor = new Color();
		public Color SelectedColor = new Color();
		private List<Button> Buttons = new List<Button>();
		public List<GameObject> Sides = new List<GameObject>();
		public int StartIndex = 0;
		public int CurrentIndex = 0;
		private int OldIndex = -1;

		void Start() {
			Buttons = new List<Button>(MenuBar.GetComponentsInChildren<Button>());
			for(int i = 0; i < Buttons.Count; i++) {
				if(i >= Sides.Count)
					break;
				int index = i;
				Buttons[i].onClick.AddListener(delegate {
					CurrentIndex = index;
				});
			}
			ShowSide(StartIndex);
		}

		void Update() {
			if(!Application.isPlaying && (CurrentIndex != OldIndex || CurrentIndex != StartIndex)) {
				if(StartIndex >= Sides.Count)
					StartIndex = Sides.Count - 1;
				if(StartIndex < 0)
					StartIndex = 0;

				CurrentIndex = StartIndex;
				OldIndex = CurrentIndex;

				ShowSide(CurrentIndex);
			} else if(CurrentIndex != OldIndex) {
				OldIndex = CurrentIndex;
				ShowSide(CurrentIndex);
				foreach(Button button in Buttons) {
					button.gameObject.GetComponentInChildren<Image>().color = DefaultColor;
				}
				Buttons[CurrentIndex].gameObject.GetComponentInChildren<Image>().color = SelectedColor;
			}
		}

		private void HideAllSides() {
			foreach(GameObject side in Sides) {
				side.SetActive(false);
			}
		}

		public void ShowSide(int index) {
			HideAllSides();
			Sides[index].SetActive(true);
		}

	}
}