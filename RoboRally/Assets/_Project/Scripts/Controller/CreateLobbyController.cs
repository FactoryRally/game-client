using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CreateLobbyController : MonoBehaviour {

	void Start() {

	}

	void Update() {

	}

	public void CreateLobby() {
		string cmdText = "D:\\Coding\\Projekte\\FactoryRally\\game-controller\\server\\src\\Tgm.Roborally.Server\\bin\\Debug\\netcoreapp3.1\\Tgm.Roborally.Server.exe";
		Process CMD = System.Diagnostics.Process.Start("CMD.exe", cmdText);
	}
}
