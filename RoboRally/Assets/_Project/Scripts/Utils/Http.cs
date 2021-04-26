using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using JetBrains.Annotations;
using MyBox;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace RoboRally.Utils {
	public class Http {

		public static Process server;

		public static bool   running  = false;
		public static string address  = "localhost";
		public static int    port     = 5050;
		public static string protocol = "http";
		public static string basePath => $"{protocol}://{address}:{port}/v1/";

		public static string serverPath = "";

		public static bool IsDebug = true;

		public static Dictionary<string, object> AuthOnlyParams => new Dictionary<string, object>(){{"pat", IngameData.JoinData.Pat}};

		public static Dictionary<string, object> Auth(Dictionary<string, object> parameters) {
			parameters.Add("pat",IngameData.JoinData.Pat);
			return parameters;
		}

		public static void StartServer() {
			if(PortInUse(port))
				return;
			Http.running = true;  
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = true;
			startInfo.FileName = serverPath;
			if(IsDebug) {
				startInfo.WindowStyle = ProcessWindowStyle.Normal;
			} else {
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			}

			try {
				server = Process.Start(startInfo);
				server.Exited += new System.EventHandler(OnServerStop);
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

		private static bool PortInUse(int port) {
			// Source: https://softwarebydefault.com/2013/02/22/port-in-use/
			bool inUse = false;

			IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
			IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


			foreach(IPEndPoint endPoint in ipEndPoints) {
				if(endPoint.Port == port) {
					inUse = true;
					break;
				}
			}


			return inUse;
		}

		public static string GetLocalIPAddress() {
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach(var ip in host.AddressList) {
				if(ip.AddressFamily == AddressFamily.InterNetwork) {
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}

		public static UnityWebRequest CreateGet(string path, Dictionary<string,object> query) {
			UnityWebRequest uwr = UnityWebRequest.Get(basePath + path + GetParameters(query));
			uwr.method = UnityWebRequest.kHttpVerbGET;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static UnityWebRequest CreatePost(string path, Dictionary<string,object> query, string[] body = null) {
			byte[] data = Encoding.ASCII.GetBytes(GetBodyJson(body));
			UnityWebRequest uwr = new UnityWebRequest(
				basePath + path + GetParameters(query),
				UnityWebRequest.kHttpVerbPOST,
				new DownloadHandlerBuffer(),
				body == null || body.Length == 0 ? null : (UploadHandler) new UploadHandlerRaw(data)
			);
			uwr.method = UnityWebRequest.kHttpVerbPOST;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static UnityWebRequest CreatePut(string path, Dictionary<string,object> query = null, string[] body = null) {
			byte[] data = Encoding.ASCII.GetBytes(GetBodyJson(body));
			UnityWebRequest uwr = new UnityWebRequest(
				basePath + path + GetParameters(query),
				UnityWebRequest.kHttpVerbPUT,
				new DownloadHandlerBuffer(),
				body == null || body.Length == 0 ? null : (UploadHandler) new UploadHandlerRaw(data)
			);
			uwr.method = UnityWebRequest.kHttpVerbPUT;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static UnityWebRequest CreateDelete(string address, string path, Dictionary<string,object> query) {
			UnityWebRequest uwr;
			if(address == null) {
				address = Http.address;
			}
			uwr = UnityWebRequest.Delete(basePath + path + GetParameters(query));
			uwr.method = UnityWebRequest.kHttpVerbDELETE;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		private static string GetParameters(string[] parameter) {
			if(parameter == null || parameter.Length == 0)
				return "";
			string body = "?";
			
			foreach (string t in parameter) {
				if(t != null)
					body += t + "&";
			}
			body = body.Substring(0, body.Length - 1);
			
			return body;
		}
		
		private static string GetParameters(Dictionary<string,object> parameter) {
			if(parameter == null || parameter.Count == 0)
				return "";
			string query = "?";
			parameter
				.Select(e => $"{e.Key}={UnityWebRequest.EscapeURL(e.Value == null ?"":e.Value.ToString())}&")
				.ForEach(e => query = $"{query}{e}");
			query = query.Substring(0, query.Length - 1);
		
			return query;
		}

		private static WWWForm GetBody(string[] bodys) {
			if(bodys == null)
				return new WWWForm();
			WWWForm form = new WWWForm();
			foreach (string t in bodys) {
				if(t.Split('=')[1].Length == 0)
					continue;
				form.AddField(t.Split('=')[0], t.Split('=')[1]);
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

		public static IEnumerator Send(UnityWebRequest uwr,Action<UnityWebRequest> OnSuccess = null,Action<UnityWebRequest> OnError = null) {
			yield return uwr.SendWebRequest();
			if (!uwr.isHttpError)
				OnSuccess?.Invoke(uwr);
			else {
				if(uwr.downloadHandler == null)
					Debug.Log($"[WARNING] Http Request failed ({uwr.error}) : {uwr.url} (NO CONTENT)");
				else
					Debug.Log($"[WARNING] Http Request failed ({uwr.error}) : {uwr.url}\n{uwr.downloadHandler.text}");
				OnError?.Invoke(uwr);
			}
		}

		public static IEnumerator SendWithCallback<T>(UnityWebRequest request, [CanBeNull] Action<T> action, Action<UnityWebRequest> onError = null) {
			return Send(request, response => {
				if (response.downloadHandler == null) {
					Debug.Log($"[WARNING] Http Request didn't produced content \'{response.url}\'");
					onError?.Invoke(request);
				}
				else {
					T body = JsonConvert.DeserializeObject<T>(response.downloadHandler.text);
					action?.Invoke(body);
				}
			}, onError);
		}
	}
}