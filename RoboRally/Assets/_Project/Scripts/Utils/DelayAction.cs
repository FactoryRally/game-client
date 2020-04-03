using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAction {

	public DelayAction(MonoBehaviour gameObject, float time, Action task) {
		gameObject.StartCoroutine(ExecuteAfterTime(time, task));
	}

	public IEnumerator ExecuteAfterTime(float time, Action task) {
		yield return new WaitForSeconds(time);
		task.Invoke();
	}
}
