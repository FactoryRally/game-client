using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Http {

    public static bool running = false;
	public const string address = "http://localhost:5050/v1/";


	public static IEnumerator Request(string path, string[] parameter, Action<UnityWebRequest> response) {
		UnityWebRequest uwr = UnityWebRequest.Get(Http.address + path + GetParameters(parameter));
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		yield return uwr.SendWebRequest();
		response(uwr);
		while(!uwr.isDone) {
			yield return null;
		}
	}

	public static IEnumerator Post(string path, string[] bodys, Action<UnityWebRequest> response) {
		UnityWebRequest uwr = UnityWebRequest.Put(Http.address + path, GetBodyJson(bodys));
		uwr.method = UnityWebRequest.kHttpVerbPOST;
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		yield return uwr.SendWebRequest();
		response(uwr);
		while(!uwr.isDone) {
			yield return null;
		}
	}

	public static IEnumerator Put(string path, string[] parameter, Action<UnityWebRequest> response) {
		UnityWebRequest uwr = UnityWebRequest.Put(Http.address + path + GetParameters(parameter), "");
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		yield return uwr.SendWebRequest();
		response(uwr);
		while(!uwr.isDone) {
			yield return null;
		}
	}

	public static IEnumerator Delete(string path, Action<UnityWebRequest> response) {
		UnityWebRequest uwr = UnityWebRequest.Delete(Http.address + path);
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		yield return uwr.SendWebRequest();
		response(uwr);
		while(!uwr.isDone) {
			yield return null;
		}
	}

	private static string GetParameters(string[] parameter) {
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
		WWWForm form = new WWWForm();
		for(int i = 0; i < bodys.Length; i++) {
			if(bodys[i].Split('=')[1].Length == 0)
				continue;
			form.AddField(bodys[i].Split('=')[0], bodys[i].Split('=')[1]);
		}
		return form;
	}

	private static string GetBodyJson(string[] bodys) {
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
