using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RoboRally.Utils {
	public class Http {

		public static Process server;

		public static bool running = false;
		public const string address = "localhost";
		public const string port = ":5050";

		public static string serverPath = "";

		public static bool IsDebug = true;


		public static void StartServer() {
			if(PortInUse(5050))
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

		public static bool PortInUse(int port) {
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

		public static UnityWebRequest CreateGet(string address, string path, params string[] query) {
			UnityWebRequest uwr;
			if(address == null) {
				address = Http.address;
			}
			uwr = UnityWebRequest.Get("http://" + address + port + "/v1/" + path + GetParameters(query));
			uwr.method = UnityWebRequest.kHttpVerbGET;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static UnityWebRequest CreatePost(string address, string path, string[] query, string[] body) {
			UnityWebRequest uwr;
			byte[] data = Encoding.ASCII.GetBytes(GetBodyJson(body));
			if(address == null) {
				address = Http.address;
			}
			uwr = new UnityWebRequest(
				"http://" + address + port + "/v1/" + path + GetParameters(query),
				UnityWebRequest.kHttpVerbPOST,
				new DownloadHandlerBuffer(),
				body == null || body.Length == 0 ? null : (UploadHandler) new UploadHandlerRaw(data)
			);
			uwr.method = UnityWebRequest.kHttpVerbPOST;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static UnityWebRequest CreatePut(string address, string path, string[] query, string[] body) {
			UnityWebRequest uwr;
			byte[] data = Encoding.ASCII.GetBytes(GetBodyJson(body));
			if(address == null) {
				address = Http.address;
			}
			uwr = new UnityWebRequest(
				"http://" + address + port + "/v1/" + path + GetParameters(query),
				UnityWebRequest.kHttpVerbPUT,
				new DownloadHandlerBuffer(),
				body == null || body.Length == 0 ? null : (UploadHandler) new UploadHandlerRaw(data)
			);
			uwr.method = UnityWebRequest.kHttpVerbPUT;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
		}

		public static UnityWebRequest CreateDelete(string address, string path, params string[] query) {
			UnityWebRequest uwr;
			if(address == null) {
				address = Http.address;
			}
			uwr = UnityWebRequest.Delete("http://" + address + port + "/v1/" + path + GetParameters(query));
			uwr.method = UnityWebRequest.kHttpVerbDELETE;
			uwr.SetRequestHeader("Content-Type", "application/json");
			uwr.SetRequestHeader("Accept", "application/json");
			return uwr;
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