using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.UI;

public class MessageOverlay : MonoBehaviour {
    private Text  _text;
    private float _secsLeft = 0;
    public  int   secondsVisible = 4;
    // Start is called before the first frame update
    void Start() {
        _text = GetComponent<Text>();
    }

    private void Update() {
        if (_secsLeft > 0)
            _secsLeft -= Time.deltaTime;
        if (_secsLeft <= 0)
            _text.enabled = false;
        else
            _text.enabled = true;
    }

    public void Display(string text, Color color) {
        _text.text  = text;
        _text.color = color;
        _secsLeft    = secondsVisible;
    }
    public void Display(string text) {
        Display(text,Color.white);
    }

    public void DisplayPhase(GamePhaseChangedEvent ev) {
        if(ev.Phase == RoundPhase.Upgrade || ev.Phase == RoundPhase.Programming)
            Display("ITS : "+ev.Phase);
    }
}
