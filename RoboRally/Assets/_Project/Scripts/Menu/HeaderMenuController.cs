using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using TMPro;
using UnityEngine;

public class HeaderMenuController : MonoBehaviour {

	public TMP_Text HeaderPhaseText;
	public TMP_Text HeaderTimerText;

	private TimeSpan TimerTime = TimeSpan.Zero;

	void Start() {

	}


	void Update() {
		if(TimerTime.TotalSeconds > 0) {
			TimerTime = TimerTime.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
			if(TimerTime.TotalSeconds < 0)
				TimerTime = TimeSpan.Zero;
			HeaderTimerText.text = $"{(int) TimerTime.TotalMinutes:00}:{TimerTime.Seconds:00}";
		}
	}


	public void OnPhaseChange(GamePhaseChangedEvent ev) {
		if(HeaderPhaseText && ev.Phase.ToString().Length > 0)
			HeaderPhaseText.text = ev.Phase.ToString();
		JObject info = ev.Information as JObject;
		if(info != null && info.ContainsKey("timer")) {
			TimerTime = TimeSpan.FromMilliseconds((long) info["timer"]);
		}
	}

	public void OnProgrammingTimerStart(ProgrammingTimerStartEvent ev) {
		TimerTime = TimeSpan.FromMilliseconds(ev.End - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
	}
}
