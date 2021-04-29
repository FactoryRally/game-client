using Newtonsoft.Json;
using RoboRally.Controller;
using RoboRally.Menu.Elements;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoboRally.Menu {
	public class HostMenuController : MonoBehaviour {

		public TMP_InputField inputField;
		public Transform Container;
		public GameObject hostItem;
		private List<GameObject> hostItems = new List<GameObject>();
		public LobbyManager lm;

		private string SAVE_FOLDER;


		void Start() {
			GameObject obj = GameObject.FindGameObjectWithTag("LobbyController");
			if(obj)
				lm = obj.GetComponent<LobbyManager>();
			SAVE_FOLDER = Application.streamingAssetsPath;
			LoadHosts();
		}

		void Update() {

		}


		public void AddHost() {
			string ip = inputField.text;
			if(lm.AddHost(ip)) {
				inputField.text = "";
				GameObject item = Instantiate(hostItem, Container);
				HostItem hi = item.GetComponent<HostItem>();
				hi.SetOnClick(this);
				hi.ip = ip;
				hostItems.Add(item);
			}
			SaveHosts();
		}

		public void RemoveHost(string ip) {
			foreach(GameObject item in hostItems) {
				HostItem hi = item.GetComponent<HostItem>();
				if(hi.ip == ip) {
					hostItems.Remove(item);
					lm.RemoveHost(ip);
					Destroy(item);
					break;
				}
			}
			SaveHosts();
		}

		public void SaveHosts() {
			if(!Directory.Exists(SAVE_FOLDER)) {
				Directory.CreateDirectory(SAVE_FOLDER);
			}
			List<string> ips = new List<string>();
			foreach(GameObject item in hostItems) {
				HostItem hi = item.GetComponent<HostItem>();
				ips.Add(hi.ip);
			}
			string json = JsonConvert.SerializeObject(ips, Formatting.Indented);
			File.WriteAllText(SAVE_FOLDER + "/hosts.json", json);
		}

		public void LoadHosts() {
			if(File.Exists(SAVE_FOLDER + "/hosts.json")) {
				string saveString = File.ReadAllText(SAVE_FOLDER + "/hosts.json");
				List<string> ips = JsonConvert.DeserializeObject<List<string>>(saveString);
				if(ips == null)
					return;
				foreach(string ip in ips) {
					GameObject item = Instantiate(hostItem, Container);
					HostItem hi = item.GetComponent<HostItem>();
					hi.SetOnClick(this);
					hi.ip = ip;
					hostItems.Add(item);
				}
			}
		}
	}
}