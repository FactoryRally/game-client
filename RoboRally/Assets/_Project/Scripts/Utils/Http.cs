using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Http {

	public static Process server;

    public static bool running = false;
	public const string address = "http://localhost:5050/v1/";

	public static string serverPath = "";

	
	public static void StartServer() {
		if(Http.running)
			return;
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.CreateNoWindow = false;
		startInfo.UseShellExecute = true;
		startInfo.FileName = serverPath;
		startInfo.WindowStyle = ProcessWindowStyle.Normal;

		try {
			server = Process.Start(startInfo);
		} catch(Exception e) {
			UnityEngine.Debug.Log("Was not able to start the server!");
		}
	}

	public static void StopServer() {
		if(!Http.running)
			return;
		if(server != null)
			server.Kill();
		Http.running = false;
	}

	public static IEnumerator Request(string path, string[] parameter, Action<UnityWebRequest> response) {
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

	public static UnityWebRequest CreateRequest(string address, string path, string[] parameter) {
		UnityWebRequest uwr;
		if(address == null) {
			uwr = UnityWebRequest.Get(Http.address + path + GetParameters(parameter));
		} else {
			uwr = UnityWebRequest.Get("http://" + address + ":5050" + "/v1/" + path + GetParameters(parameter));
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
				body = parameter[i] + "&";
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
