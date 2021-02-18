using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {
	public AudioSource source;
	private AudioClip clip;

	bool isPress = false;
	int wait = 0;

	void Update() {
		if(clip == null) {
			return;
		}
		if(wait <= 0) {
			source.PlayOneShot(clip);
			clip = null;
			isPress = false;
		}
		wait--;
	}

	public void PlaySound(AudioClip clip) {
		wait = 0;
		isPress = true;
		this.clip = clip;
	}

	public void PlaySoundSelect(AudioClip clip) {
		wait = 4;
		if(!isPress)
			this.clip = clip;
	}
}
