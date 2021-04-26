using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DontDestroy : MonoBehaviour {
	private static bool exist = false;
	
	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		if (exist)
			Destroy(gameObject);
		else exist = true;
	}
}
