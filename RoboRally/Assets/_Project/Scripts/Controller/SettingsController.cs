using Newtonsoft.Json;
using RoboRally.Scripts.Utils;
using RoboRally.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsController : MonoBehaviour {

	private static SettingsController _instance;
	public static SettingsController Instance { get { return _instance; } }
	public static string SAVE_FOLDER = "";

	void Awake() {
		if(_instance != null && _instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
			DontDestroyOnLoad(this);
		}
		SAVE_FOLDER = Application.streamingAssetsPath;
		if(File.Exists(SAVE_FOLDER + "/settings.json")) {
			string saveString = File.ReadAllText(SAVE_FOLDER + "/settings.json");
			Settings settings = JsonConvert.DeserializeObject<Settings>(saveString);
			if(settings == null)
				settings = new Settings();
		} else {	
			Settings settings = new Settings();
		}
		GraphicSettings.Setup();
	}

	void Start() {
	}

	void Update() {
		Http.IsDebug = Settings.Instance.IsDebug;
	}

	private void OnApplicationQuit() {
		Http.StopServer();
		SaveSettings();
	}

	public void SaveSettings() {
		if(!Directory.Exists(SAVE_FOLDER)) {
			Directory.CreateDirectory(SAVE_FOLDER);
		}
		string json = JsonConvert.SerializeObject(Settings.Instance, Formatting.Indented);
		File.WriteAllText(SAVE_FOLDER + "/settings.json", json);
	}
}
