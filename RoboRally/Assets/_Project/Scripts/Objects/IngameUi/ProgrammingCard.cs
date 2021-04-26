using System;
using System.Collections;
using System.Collections.Generic;
using Tgm.Roborally.Api.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnChoose(int card);
public class ProgrammingCard : MonoBehaviour{

    private OnChoose _choose;
    public OnChoose onChoose {
        set => _choose = value;
    }
    public int CardId {
        get => _cardId;
        set {
            _cardId = value;
            _controller.GetCommand(_cardId, UpdateUi);
        }
    }

    private int                _cardId;
    public  Text               title;
    public  Image              img;
    public  Text               description;
    private GameItemController _controller;
    private RectTransform      _transform;
    
    // Start is called before the first frame update
    private void Awake() {
        _controller = FindObjectOfType<GameItemController>();
        _transform  = GetComponent<RectTransform>();
        GetComponent<Button>().onClick.AddListener(OnMouseDown);
    }
    
    public void OnMouseEnter()
    {
        _transform.localScale.Set(1.75f,1.75f,1.75f);
    }

    private void OnMouseExit() {
        _transform.localScale.Set(1,1,1);
    }
    private void OnMouseDown() {
        Debug.Log("Clicky : "+_choose+"("+_cardId+")");
        _choose(_cardId);
    }
    

    private void UpdateUi(RobotCommand card) {
        title.text       = card.Name;
        description.text = card.Description;
        card.Parameters.ForEach(e => description.text = description.text.Replace($"{{{e.Name}}}",e.Value.ToString()));
    }
}
