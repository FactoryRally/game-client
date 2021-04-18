using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoboRally.Menu.Elements {
	[RequireComponent(typeof(Button))]
	public class ButtonSound : MonoBehaviour {

		public AudioSource source;
		public AudioClip clip;
		public float delay = 0;
		private Button button;

		bool isPress = false;
		float wait = 0;


		public void Awake() {
			button = GetComponent<Button>();
			button.onClick.AddListener(() => PlaySound(delay));
			if(source == null) {
				GameObject gj = GameObject.FindGameObjectWithTag("AudioController");
				if(gj) {
					source = gj.GetComponent<AudioSource>();
				}
			}
		}

		void Update() {
			if(clip == null) {
				return;
			}
			if(wait <= Time.time && wait != 0) {
				wait = 0;
				source.PlayOneShot(clip);
				isPress = false;
			}
		}

		public void PlaySound(float delay = 0) {
			wait = Time.time + delay;
			isPress = true;
		}

	}
}