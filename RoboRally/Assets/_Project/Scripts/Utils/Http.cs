using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Utils {
	public class Http {

		public static Process server;

		public static bool running = false;
		public const string address = "http://localhost";
		public const string port = ":5050";

		public static string serverPath = "";


		public static void StartServer() {
			if(Http.running)
				return;
			Http.running = true;
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = true;
			startInfo.FileName = serverPath;
			startInfo.WindowStyle = ProcessWindowStyle.Normal;

			try {
				server = Process.Start(startInfo);
				server.Exited += new EventHandler(OnServerStop);
			} catch(Exception e) {
				UnityEngine.Debug.Log("Was not able to start the server!");
			}
		}

		public static void OnServerStop(object sender, EventArgs e) {
			Http.running = false;
		}

		public static void StopServer() {
			if(!Http.running)
				return;
			if(server != null && !server.HasExited)
				server.Kill();
			Http.running = false;
		}

		public static IEnumerator Get(string path, string[] parameter, Action<UnityWebRequest> response) {
			UnityWebRequest uwr = UnityWebRequest.Get(Http.address + path + GetParameters(parameter));
			uwr.method = UnityWebRequest.kHttpVerbGET;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			yield return uwr.SendWebRequest();
			while(!uwr.isDone) {
				yield return null;
			}
			response(uwr);
		}

		public static UnityWebRequest CreateGet(string address, string path, params string[] query) {
			UnityWebRequest uwr;
			if(address == null) {
				uwr = UnityWebRequest.Get(Http.address + path + GetParameters(query));
			} else {
				uwr = UnityWebRequest.Get("http://" + address + port + "/v1/" + path + GetParameters(query));
			}
			uwr.method = UnityWebRequest.kHttpVerbGET;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static IEnumerator Post(string path, string[] bodys, Action<UnityWebRequest> response) {
			UnityWebRequest uwr = UnityWebRequest.Put(Http.address + path, GetBodyJson(bodys));
			uwr.method = UnityWebRequest.kHttpVerbPOST;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			yield return uwr.SendWebRequest();
			while(!uwr.isDone) {
				yield return null;
			}
			response(uwr);
		}

		public static UnityWebRequest CreatePost(string address, string path, string[] query, string[] body) {
			UnityWebRequest uwr;
			byte[] data = Encoding.ASCII.GetBytes(GetBodyJson(body));
			if(address == null) {
				uwr = new UnityWebRequest(
					Http.address + port + "/v1/" + path + GetParameters(query),
					"POST",
					new DownloadHandlerBuffer(),
					body == null || body.Length == 0 ? null : (UploadHandler) new UploadHandlerRaw(data)
				);
			} else {
				uwr = new UnityWebRequest(
					"http://" + address + port + "/v1/" + path + GetParameters(query),
					"POST",
					new DownloadHandlerBuffer(),
					body == null || body.Length == 0 ? null : (UploadHandler) new UploadHandlerRaw(data)
				);
			}
			uwr.method = UnityWebRequest.kHttpVerbPOST;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}


		public static IEnumerator Put(string path, string[] parameter, Action<UnityWebRequest> response) {
			UnityWebRequest uwr = UnityWebRequest.Put(Http.address + path + GetParameters(parameter), "");
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			yield return uwr.SendWebRequest();
			while(!uwr.isDone) {
				yield return null;
			}
			response(uwr);
		}

		public static UnityWebRequest CreatePut(string address, string path, string[] query, string[] body) {
			UnityWebRequest uwr;
			if(address == null) {
				uwr = UnityWebRequest.Put(Http.address + port + "/v1/" + path + GetParameters(query), GetBody(body).data);
			} else {
				uwr = UnityWebRequest.Put("http://" + address + port + "/v1/" + path + GetParameters(query), GetBody(body).data);
			}
			uwr.method = UnityWebRequest.kHttpVerbPUT;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static IEnumerator Delete(string path, Action<UnityWebRequest> response) {
			UnityWebRequest uwr = UnityWebRequest.Delete(Http.address + path);
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			yield return uwr.SendWebRequest();
			while(!uwr.isDone) {
				yield return null;
			}
			response(uwr);
		}

		private static string GetParameters(string[] parameter) {
			if(parameter == null)
				return "";
			string body = "?";
			if(parameter.Length == 0) {
				body = "";
			} else {
				for(int i = 0; i < parameter.Length; i++) {
					if(parameter[i] != null)
						body += parameter[i] + "&";
				}
				body = body.Substring(0, body.Length - 1);
			}
			return body;
		}

		private static WWWForm GetBody(string[] bodys) {
			if(bodys == null)
				return new WWWForm();
			WWWForm form = new WWWForm();
			for(int i = 0; i < bodys.Length; i++) {
				if(bodys[i].Split('=')[1].Length == 0)
					continue;
				form.AddField(bodys[i].Split('=')[0], bodys[i].Split('=')[1]);
			}
			return form;
		}

		private static string GetBodyJson(string[] bodys) {
			if(bodys == null)
				return "";
			string json = "{\n";
			for(int i = 0; i < bodys.Length; i++) {
				if(bodys[i].Split('=')[1].Length == 0)
					continue;
				string value;
				value = "\"" + bodys[i].Split('=')[1] + "\"";
				try {
					int.Parse(bodys[i].Split('=')[1]);
					value = bodys[i].Split('=')[1];
				} catch(Exception) { }
				try {
					bool.Parse(bodys[i].Split('=')[1]);
					value = bodys[i].Split('=')[1];
				} catch(Exception) { }
				if(i + 1 < bodys.Length) {
					json += "  \"" + bodys[i].Split('=')[0] + "\": " + value + ",\n";
				} else {
					json += "  \"" + bodys[i].Split('=')[0] + "\": " + value + "\n";
				}
			}
			json += "}";
			return json;
		}
	}
}