using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAction {

	private bool stopRepeat = false;
	public int currentRepeat = 0;

	public DelayAction() { }

	public DelayAction(MonoBehaviour gameObject, float time, Action task) {
		gameObject.StartCoroutine(ExecuteAfterTime(time, task));
	}

	public DelayAction(MonoBehaviour gameObject, float time, int repeat, Action task) {
		gameObject.StartCoroutine(RepeatAfterTime(time, repeat, task));
	}

	private IEnumerator ExecuteAfterTime(float time, Action task) {
		yield return new WaitForSeconds(time);
		task.Invoke();
	}

	private IEnumerator RepeatAfterTime(float time, int repeat, Action task) {
		for (int i = 0; i < repeat; i++) {
			currentRepeat = i+1;
			if(stopRepeat)
				break;
			yield return new WaitForSeconds(time);
			task.Invoke();
		}
	}

	public void StopRepeat() {
		stopRepeat = true;
	}
}
